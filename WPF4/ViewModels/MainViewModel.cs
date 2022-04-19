using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using DynamicData;
using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using WPF4.Models;
using WPF4.Services;

namespace WPF4.ViewModels
{
    public sealed class MainViewModel : ReactiveObject
    {
        #region OAPH & Commands

        [Reactive]
        public int TierA1 { get; set; } = 1;

        [Reactive]
        public int TierA2 { get; set; } = 2;

        [Reactive]
        public int TierA3 { get; set; } = 3;

        // Вычисляемое свойство
        public int TierB1 => _tierB1.Value;

        // Вычисляемое свойство
        public int TierB2 => _tierB2.Value;

        public ReactiveCommand<Unit, Unit> ZeroCommand { get; }

        private void ZeroImpl()
        {
            TierA1 = 0;
            TierA2 = 0;
            TierA3 = 0;
        }

        private readonly ObservableAsPropertyHelper<int> _tierB1;
        private readonly ObservableAsPropertyHelper<int> _tierB2;

        #endregion

        #region DynamicData & Interactions

        public ReadOnlyObservableCollection<Person> Persons { get; }

        [Reactive]
        public Person? SelectedPerson { get; set; }

        public ReactiveCommand<Unit, Unit> Update { get; }

        public ReactiveCommand<Unit, Unit> Add { get; }

        public ReactiveCommand<Unit, Unit> Remove { get; }

        public Interaction<Unit, Person?> CreatePerson { get; } = new();

        private async Task UpdateImpl()
        {
            await _personListService.Update();
        }

        private async Task AddImpl()
        {
            var person = await CreatePerson.Handle(Unit.Default);
            if (person is null)
                return;

            await _personListService.Add(person);
        }

        private async Task RemoveImpl()
        {
            if (SelectedPerson is not { } person)
                return;

            await _personListService.Remove(person);
        }

        private readonly PersonListService _personListService;

        #endregion

        public MainViewModel(PersonListService personListService)
        {
            // OAPH & Commands
            {
                _tierB1 = this.WhenAnyValue(o => o.TierA1, o => o.TierA2, (a1, a2) => a1 + a2)
                    .ToProperty(this, o => o.TierB1);

                _tierB2 = this.WhenAnyValue(o => o.TierA2, o => o.TierA3, (a2, a3) => a2 + a3)
                    .ToProperty(this, o => o.TierB2);

                var canZeroTierA = this
                    .WhenAnyValue(o => o.TierA1, o => o.TierA2, o => o.TierA3, (a1, a2, a3) => a1 != 0 || a2 != 0 || a3 != 0);

                ZeroCommand = ReactiveCommand.Create(ZeroImpl, canZeroTierA);
            }

            // DynamicData & Interactions
            {
                _personListService = personListService;
                _ = _personListService
                    .Connect()
                    .Sort(SortExpressionComparer<Person>.Ascending(o => o.ToFullName()))
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Bind(out var persons)
                    .Subscribe();
                Persons = persons;

                var canExecute = new Subject<bool>();
                var isSelected = this.WhenAnyValue(o => o.SelectedPerson, (Person? o) => o is not null);
                var canExecuteAndIsSelected = canExecute.CombineLatest(isSelected,
                    (canExecute, isSelected) => canExecute && isSelected);

                Update = ReactiveCommand.CreateFromTask(() => ExclusiveWrapper(UpdateImpl), canExecute);
                Add = ReactiveCommand.CreateFromTask(() => ExclusiveWrapper(AddImpl), canExecute);
                Remove = ReactiveCommand.CreateFromTask(() => ExclusiveWrapper(RemoveImpl), canExecuteAndIsSelected);

                async Task ExclusiveWrapper(Func<Task> impl)
                {
                    try
                    {
                        canExecute.OnNext(false);
                        await impl();
                    }
                    finally
                    {
                        canExecute.OnNext(true);
                    }
                }

                canExecute.OnNext(true);
            }
        }
    }
}
