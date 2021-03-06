using Avalonia;

namespace Avalonia3
{
    internal static class Program
    {
        public static int Main(string[] args) =>
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .LogToTrace()
                .UsePlatformDetect();
    }
}
