using System.Reactive;

using Avalonia.ReactiveUI;

using ReactiveUI;

using Shared4.ViewModels;

namespace Avalonia4
{
    public partial class PersonWindow : ReactiveWindow<PersonViewModel>
    {
        public PersonWindow()
        {
            InitializeComponent();

            _ = this.WhenActivated(cd =>
              {
                  if (ViewModel is null)
                      return;

                  cd.Add(ViewModel.Close.RegisterHandler(interaction =>
                  {
                      interaction.SetOutput(Unit.Default);
                      Close(interaction.Input);
                  }));
              });
        }
    }
}
