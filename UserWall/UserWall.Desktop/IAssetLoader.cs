using System.Reflection;

using Avalonia.Media.Imaging;

namespace UserWall.Desktop;

public interface IAssetLoader
{
    Bitmap? LoadBitmap(string path);
    Bitmap? LoadBitmap(string path, Assembly assembly);
}
