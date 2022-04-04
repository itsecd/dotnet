using System;
using System.IO;
using System.Reflection;

using Avalonia;
using Avalonia.Platform;

namespace Avalonia2
{
    internal static class ResourceLoader
    {
        public static Stream? Open(string path)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            if (assets is null)
                // Log missing service
                return null;

            try
            {
                var assemblyName = Assembly.GetCallingAssembly().GetName().Name;
                return assets.Open(new Uri($"avares://{assemblyName}/{path}"));
            }
            catch (Exception e)
            {
                // Log missing resource
                return null;
            }
        }
    }
}
