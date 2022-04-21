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

        public Person? Result { get; private set; }

        public ReactiveCommand<Unit, Unit> Ok { get; }

        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public Interaction<bool, Unit> Close { get; } = new(RxApp.MainThreadScheduler);

        public PersonViewModel()
        {
            Ok = ReactiveCommand.CreateFromObservable(OkImpl);
            Cancel = ReactiveCommand.CreateFromObservable(CancelImpl);
        }

        private IObservable<Unit> OkImpl()
        {
            // Если нужна валидация, она делается здесь
            // 1. Завести свойство ShowError типа Interaction<string, Unit> для показа ошибки
            // 2. В PersonWindow.xaml.cs зарегистрировать для него обработчик
            // 3. Здесь написать что-то вроде:
            //     if (что-то пошло не так)
            //         return ShowError.Handle(сообщение для показа пользователю);

            Result = new Person
            {
                FirstName = FirstName,
                LastName = LastName,
                IsFemale = IsFemale
            };

            return Close.Handle(true);
        }

        private IObservable<Unit> CancelImpl()
        {
            Result = null;

            return Close.Handle(false);
        }
    }
}
