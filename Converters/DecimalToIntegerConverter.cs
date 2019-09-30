using System;
using System.Globalization;

namespace SWAM.Converters
{
    /// <summary>
    /// Convert decimal number to Int64
    /// </summary>
    public class DecimalToIntegerConverter : BaseValueConverter<DecimalToIntegerConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)
            {
                return Decimal.ToInt64(price);
            }
            else
                return value;
      
               
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Int64 price)
            {
                return (decimal)price;
            }
            else
                return value;
        }
    }
}
