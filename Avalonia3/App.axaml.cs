using System.Collections.ObjectModel;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Avalonia3
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var model = new ObservableCollection<Person>
                {
                    new Person {FirstName = "Megan", LastName = "Roberts", IsFemale = true},
                    new Person {FirstName = "Michael", LastName = "Campbell"},
                    new Person {FirstName = "Cindy", LastName = "Benson", IsFemale = true},
                    new Person {FirstName = "Christina", LastName = "Hampton", IsFemale = true},
                    new Person {FirstName = "Timothy", LastName = "Carroll"},
                    new Person {FirstName = "Joshua", LastName = "Nichols"},
                    new Person {FirstName = "Bradley", LastName = "Gonzalez"},
                };
                var window = new MainWindow {DataContext = model};
                desktop.MainWindow = window;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
