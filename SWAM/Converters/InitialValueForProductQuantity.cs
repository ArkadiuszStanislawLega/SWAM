using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SWAM.Converters
{
    public class InitialValueForProductQuantity :BaseValueConverter<InitialValueForProductQuantity>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value != 0)
                return false;
             return value = 1;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
