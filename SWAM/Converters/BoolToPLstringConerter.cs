using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class BoolToPLstringConerter : BaseValueConverter<BoolToPLstringConerter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                if (status) return "opłacono";
                else return "nie opłacono";
            }
            else return value.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
