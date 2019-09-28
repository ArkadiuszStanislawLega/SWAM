using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Converters
{
    public class VisibilityReverseConverter : BaseValueConverter<VisibilityReverseConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is System.Windows.Visibility visibility && visibility == System.Windows.Visibility.Visible)
            {
                return System.Windows.Visibility.Collapsed;
            }
            else
                return System.Windows.Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
