using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Converters
{
    /// <summary>
    /// Date converter to date with polish names of months.
    /// </summary>
    public class DateConverter : BaseValueConverter<DateConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                var culutreInfo = CultureInfo.CreateSpecificCulture("pl-PL");
                //To change format of date change "F" for any from "d", "D", "f", "F", "g", "G", "m", "o", "r", "s", "t", "T", "u", "U", "Y"
                //More information about it - documentation - https://docs.microsoft.com/pl-pl/dotnet/api/system.datetime.tostring?view=netframework-4.8
                return date.ToString("F", culutreInfo);
            }
            else
                return null;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
