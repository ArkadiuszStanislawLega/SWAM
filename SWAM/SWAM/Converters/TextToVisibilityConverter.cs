using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SWAM.Converters
{
    /// <summary>
    /// Converter to change text with number of messages to visibility in mail button.
    /// When messages is 0 visibility are hidden.
    /// </summary>
    public class TextToVisibilityConverter : BaseValueConverter<TextToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueString = (string)value;

            if (valueString == "") return Visibility.Hidden;
            else return  Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
