using System.Collections.ObjectModel;
using System.Windows;

namespace WPF3
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new ObservableCollection<Person> {
                new Person { FirstName="Megan", LastName="Roberts", IsFemale = true },
                new Person { FirstName="Michael", LastName="Campbell" },
                new Person { FirstName="Cindy", LastName="Benson", IsFemale = true  },
                new Person { FirstName="Christina", LastName="Hampton", IsFemale = true },
                new Person { FirstName="Timothy", LastName="Carroll" },
                new Person { FirstName="Joshua", LastName="Nichols" },
                new Person { FirstName="Bradley", LastName="Gonzalez" },
            };

            var main = new MainWindow { DataContext = model };
            main.Show();
            Current.MainWindow = main;
        }
    }
}
