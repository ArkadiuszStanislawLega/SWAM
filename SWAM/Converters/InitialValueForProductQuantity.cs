using System;
using System.Globalization;
using System.Windows.Data;

namespace SWAM.Converters
{
    public class InitialValueForProductQuantity :BaseValueConverter<InitialValueForProductQuantity>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value = 1;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
