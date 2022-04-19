using System.Reactive.Linq;

using ReactiveUI;

using WPF4.ViewModels;

namespace WPF4.Views
{
    // Привет багам по nullable-совместимости WPF со сторонними библиотеками
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

                    // Привет отсутствию асинхронного ShowDialog в WPF
                    return Observable.Start(() =>
                    {
                        _ = personView.ShowDialog();
                        interaction.SetOutput(personViewModel.Result);
                    }, RxApp.MainThreadScheduler);
                }));
            });
        }
    }
}
