using System.Net.Http;

using Avalonia;
using Avalonia.ReactiveUI;

using Splat;

namespace UserWall.Desktop;

internal class Program
{
    public static void Main(string[] args)
    {
        var configuration = new Configuration();
        var client = new UserWallClient("http://localhost:5159", new HttpClient());

        Locator.CurrentMutable.RegisterConstant(configuration);
        Locator.CurrentMutable.RegisterConstant(client);
        Locator.CurrentMutable.RegisterConstant<IAssetLoader>(new AssetLoader());

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI()
            .LogToTrace();
    }
}
