using Avalonia;
using Avalonia.ReactiveUI;

namespace UserWall.Desktop;

internal class Program
{
    public static void Main(string[] args)
    {
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
