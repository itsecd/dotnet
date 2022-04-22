using System;
using System.Reactive;

using ReactiveUI;

using Shared4.Models;

namespace Shared4.ViewModels
{
    public sealed class PersonViewModel : ReactiveObject
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool IsFemale { get; set; }

        public ReactiveCommand<Unit, Unit> Ok { get; }

        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public Interaction<Person?, Unit> Close { get; } = new(RxApp.MainThreadScheduler);

        public PersonViewModel()
        {
            Ok = ReactiveCommand.CreateFromObservable(OkImpl);
            Cancel = ReactiveCommand.CreateFromObservable(CancelImpl);
        }

        private IObservable<Unit> OkImpl()
        {
            // Validation may be here

            var person = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                IsFemale = IsFemale
            };

            return Close.Handle(person);
        }

        private IObservable<Unit> CancelImpl()
        {
            return Close.Handle(null);
        }
    }
}
