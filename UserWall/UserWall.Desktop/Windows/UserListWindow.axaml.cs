using Avalonia.ReactiveUI;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop;

public partial class UserListWindow : ReactiveWindow<UserListViewModel>
{
    public UserListWindow()
    {
        InitializeComponent();
    }
}
