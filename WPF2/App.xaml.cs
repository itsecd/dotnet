using System.Collections.ObjectModel;
using System.Windows;

namespace WPF2
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var model = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 };
            var main = new MainWindow { DataContext = model };
            main.Show();
            Current.MainWindow = main;
        }
    }
}
