using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Shared4.Services;
using Shared4.ViewModels;

namespace Avalonia4
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var personListService = new PersonListService();
                var mainWindowViewModel = new MainViewModel(personListService);
                var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
