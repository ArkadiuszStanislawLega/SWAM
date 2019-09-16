using SWAM.Enumerators;
using System;
using System.Globalization;
using SWAM.Strings;

namespace SWAM.Converters
{
    /// <summary>
    /// Converter english names of user type to polish names.
    /// </summary>
    public class ENToPLUserTypeConverter : BaseValueConverter<ENToPLUserTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserType user)
            {
                switch (user)
                {
                    case UserType.Administrator: return PLstrings.ADMINISTRATOR;
                    case UserType.Programmer: return PLstrings.PROGRAMMER;
                    case UserType.Seller: return PLstrings.SELLER;
                    case UserType.Warehouseman: return PLstrings.WAREHOUSEMAN;
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
