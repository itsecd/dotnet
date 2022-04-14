using System;
using System.Globalization;
using Avalonia.Data.Converters;

using Avalonia.Media;

namespace Avalonia3.Converters
{
    public class PersonToGenderColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Person person)
                return Brushes.Transparent;

            return person.IsFemale ? Brushes.DeepPink : Brushes.Blue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
