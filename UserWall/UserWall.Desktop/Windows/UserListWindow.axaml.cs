using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Avalonia.ReactiveUI;

using ReactiveUI;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Views;

public partial class UserListWindow : ReactiveWindow<UserListViewModel>
{
    public UserListWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            if (ViewModel is null)
                return;

            ViewModel.ItemInteraction.RegisterHandler(Item);
        });
    }

    public async Task Item(InteractionContext<UserDto?, UserDto?> ctx)
    {
        var view = new UserWindow { ViewModel = new UserViewModel(ctx.Input) };
        await view.ShowDialog(this);
        ctx.SetOutput(null);
    }
}
