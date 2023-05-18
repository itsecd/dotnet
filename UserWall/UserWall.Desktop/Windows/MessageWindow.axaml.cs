using Avalonia.Platform;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Windows;

public partial class MessageWindow : BaseWindow<MessageViewModel>
{
    public MessageWindow()
    {
        InitializeComponent();

        ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
        ExtendClientAreaTitleBarHeightHint = -1;
        ExtendClientAreaToDecorationsHint = true;
    }
}
