using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    /// <summary>
    /// Converter english names of status of user account to polish names.
    /// </summary>
    public class ENtoPLstatusOfUserAccountConverter : BaseValueConverter<ENtoPLstatusOfUserAccountConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusOfUserAccount user)
            {
                switch (user)
                {
                    case StatusOfUserAccount.Active: return PLstrings.ACTIVE;
                    case StatusOfUserAccount.Blocked: return PLstrings.BLOCK;
                    default: return user.ToString();
                }
            }
            else return value.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
