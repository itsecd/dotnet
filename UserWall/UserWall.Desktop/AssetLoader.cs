using System;
using System.Diagnostics;
using System.Reflection;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace UserWall.Desktop;

public class AssetLoader : IAssetLoader
{
    public Bitmap? LoadBitmap(string path)
    {
        return LoadBitmap(path, Assembly.GetCallingAssembly());
    }

    public Bitmap? LoadBitmap(string path, Assembly assembly)
    {
        var assetLoader = AvaloniaLocator.Current.GetService<Avalonia.Platform.IAssetLoader>();
        if (assetLoader is null)
        {
            Debug.WriteLine("Failed to resolve Avalonia.Platform.IAssetLoader.");
            return null;
        }

        var assemblyName = assembly.GetName().Name ?? string.Empty;
        var uri = new Uri($"avares://{assemblyName}/{path}");

        try
        {
            using var stream = assetLoader.Open(uri);
            return new Bitmap(stream);
        }
        catch
        {
            Debug.WriteLine($"Failed to open {uri}.");
            return null;
        }
    }
}
