using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace SWAM.Converters
{
    /// <summary>
    /// Converting value of expire date to colour of foreground brush.
    /// </summary>
    class DateOfExpireColorConverter : System.Windows.Data.IValueConverter
    {
        Brush _returnBrash = (Brush)Application.Current.FindResource("FontsBrash");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("Obiekt docelowy musi być typu Brush");

            if ((string)value != "")
            {
                DateTime dateOfExpire = DateTime.Parse((string)value);
                this._returnBrash = DateTime.Compare(dateOfExpire, DateTime.Now) == 1 ? (Brush)Application.Current.FindResource("FontsBrash") : 
                    (Brush)Application.Current.FindResource("WarningBrash");
            }

            return this._returnBrash;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
