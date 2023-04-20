using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Avalonia.Controls;
using Avalonia.Threading;

namespace UserWall.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Task.Run(Load);
    }

    private async Task Load()
    {
        var client = new UserWallClient("http://localhost:5159", new HttpClient());
        var users = (await client.GetUsersAsync())
            .Select(user => $"[{user.Id}] {user.FirstName} {user.LastName}")
            .ToList();

        Dispatcher.UIThread.Post(() => listBox.Items = users);
    }
}
