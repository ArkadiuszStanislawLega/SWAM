using SWAM.Enumerators;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace SWAM.Converters
{
    /// <summary>
    /// Converter that checks whether the account is blocked, depending on that gives the color in which the text should be written in user profile.
    /// </summary>
    public class AccountBlockToColorConverter : BaseValueConverter<AccountBlockToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("Obiekt docelowy musi być typu Brush");

            var valueString = (string)value;

            if (valueString != "" && valueString == StatusOfUserAccount.Blocked.ToString())
                return (Brush)Application.Current.FindResource("WarningBrash");
     
            return (Brush)Application.Current.FindResource("FontsBrash");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
