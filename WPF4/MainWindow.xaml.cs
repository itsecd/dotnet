using System.Reactive.Linq;

using ReactiveUI;

using Shared4.Models;
using Shared4.ViewModels;

namespace WPF4
{
    // https://github.com/reactiveui/ReactiveUI/issues/2330#issuecomment-577968613
    public class MainWindowBase : ReactiveWindow<MainViewModel>
    {
    }

    public partial class MainWindow : MainWindowBase
    {
        public MainWindow()
        {
            InitializeComponent();

            _ = this.WhenActivated(cd =>
            {
                if (ViewModel is null)
                    return;

                cd.Add(ViewModel.CreatePerson.RegisterHandler(interaction =>
                {
                    var personViewModel = new PersonViewModel();
                    var personView = new PersonWindow
                    {
                        Owner = this,
                        ViewModel = personViewModel
                    };

                    // No async version of ShowDialog...
                    return Observable.Start(() =>
                    {
                        _ = personView.ShowDialog();
                        interaction.SetOutput(personView.Tag as Person);
                    }, RxApp.MainThreadScheduler);
                }));
            });
        }
    }
}
