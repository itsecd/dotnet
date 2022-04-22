using System.Reactive;

using ReactiveUI;

using Shared4.ViewModels;

namespace WPF4
{
    // https://github.com/reactiveui/ReactiveUI/issues/2330#issuecomment-577968613
    public class PersonWindowBase : ReactiveWindow<PersonViewModel>
    {
    }

    public partial class PersonWindow : PersonWindowBase
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
                      DialogResult = interaction.Input;
                      interaction.SetOutput(Unit.Default);
                      Close();
                  }));
              });
        }
    }
}
