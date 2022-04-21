using System;
using System.Diagnostics;
using System.Reactive;

using Avalonia;
using Avalonia.ReactiveUI;

using ReactiveUI;

namespace Avalonia4
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(exception =>
            {
                Debug.WriteLine($"RxApp Exception >>> {exception}");
            });

            return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder
                .Configure<App>()
                .LogToTrace()
                .UsePlatformDetect()
                .UseReactiveUI();
        }
    }
}
