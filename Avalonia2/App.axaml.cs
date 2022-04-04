using System.Collections.ObjectModel;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia2
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            try
            {
                using var stream = ResourceLoader.Open("Assets/App.png");
                Resources["AppIcon"] = new WindowIcon(stream);
            }
            catch
            {
                // Log io error
                using var bitmap = new WriteableBitmap(
                    new PixelSize(16, 16),
                    new Vector(96, 96),
                    PixelFormat.Bgra8888,
                    AlphaFormat.Opaque);
                Resources["AppIcon"] = new WindowIcon(bitmap);
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var model = new ObservableCollection<int> {1, 2, 3, 4, 5, 6, 7};
                var window = new MainWindow {DataContext = model};
                desktop.MainWindow = window;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
