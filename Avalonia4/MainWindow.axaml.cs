using Avalonia.ReactiveUI;

using ReactiveUI;

using Shared4.ViewModels;

namespace Avalonia4
{
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            _ = this.WhenActivated(cd =>
            {
                if (ViewModel is null)
                    return;

                cd.Add(ViewModel.CreatePerson.RegisterHandler(async interaction =>
                {
                    var personViewModel = new PersonViewModel();
                    var personView = new PersonWindow { ViewModel = personViewModel };

                    await personView.ShowDialog(this);
                    interaction.SetOutput(personViewModel.Result);
                }));
            });
        }
    }
}
