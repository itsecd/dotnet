using System;
using System.Reactive;
using System.Windows;

using ReactiveUI;

using Shared4.Services;
using Shared4.ViewModels;

namespace WPF4
{
    public partial class App : Application
    {
        public App()
        {
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(exception =>
            {
                System.Diagnostics.Debug.WriteLine($"RxApp Exception >>> {exception}");
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var personListService = new PersonListService();
            var mainWindowViewModel = new MainViewModel(personListService);
            var mainWindow = new MainWindow { ViewModel = mainWindowViewModel };
            Current.MainWindow = mainWindow;

            mainWindow.Show();
        }
    }
}
