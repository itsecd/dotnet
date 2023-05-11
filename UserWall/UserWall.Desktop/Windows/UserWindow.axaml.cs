using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Windows;

public partial class UserWindow : BaseWindow<UserViewModel>
{
    public UserWindow()
    {
        InitializeComponent();
    }
}
