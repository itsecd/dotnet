using System;
using System.Globalization;

using Avalonia.Data.Converters;

namespace Avalonia2.Converters
{
    public class IntToEvenConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is int intValue && intValue % 2 == 0;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
