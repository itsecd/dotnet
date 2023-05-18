using System;
using System.Globalization;

using Avalonia.Data.Converters;

using Splat;

using UserWall.Desktop.ViewModels;

namespace UserWall.Desktop.Converters;

public class MessagePictogramToBitmapConverter : IValueConverter
{
    private readonly IAssetLoader _assetLoader = Locator.Current.GetServiceOrThrow<IAssetLoader>();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not MessagePictogram pictogram)
            return null;

        return pictogram switch
        {
            MessagePictogram.Ask => _assetLoader.LoadBitmap("Assets/Ask.png"),
            MessagePictogram.Error => _assetLoader.LoadBitmap("Assets/Error.png"),
            MessagePictogram.Info => _assetLoader.LoadBitmap("Assets/Info.png"),
            MessagePictogram.Warning => _assetLoader.LoadBitmap("Assets/Warning.png"),
            _ => null
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
