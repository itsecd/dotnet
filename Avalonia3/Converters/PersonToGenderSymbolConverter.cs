using System;
using System.Globalization;

using Avalonia.Data.Converters;

namespace Avalonia3.Converters
{
    public class PersonToGenderSymbolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Person person)
                return '?';

            return person.IsFemale ? '♀' : '♂';
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
