using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Avalonia.ReactiveUI;

using ReactiveUI;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Windows;

public partial class UserListWindow : BaseWindow<UserListViewModel>
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
        var userDto = await view.ShowDialog<UserDto?>(this);
        ctx.SetOutput(userDto);
    }
}
