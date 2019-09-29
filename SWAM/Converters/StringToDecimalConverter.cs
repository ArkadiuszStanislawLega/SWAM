using System;
using System.Globalization;

namespace SWAM.Converters
{
    /// <summary>
    /// Convert decimal number to Int64
    /// </summary>
    public class StringToDecimalConverter : BaseValueConverter<StringToDecimalConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)return price;
            else if (decimal.TryParse((string)value, out decimal deprice)) return deprice;
            else return value;    
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
                return (string)value;
        }
    }
}
