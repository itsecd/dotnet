using Avalonia.ReactiveUI;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Views;

public partial class UserWindow : ReactiveWindow<UserViewModel>
{
    public UserWindow()
    {
        InitializeComponent();
    }
}
