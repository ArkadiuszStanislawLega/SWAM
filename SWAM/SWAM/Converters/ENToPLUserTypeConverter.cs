using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Converters
{
    /// <summary>
    /// Converter english names of user type to polish names.
    /// </summary>
    public class ENToPLUserTypeConverter : BaseValueConverter<ENToPLUserTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var v = (UserType)value;
            if (value is UserType user)
            {
                switch (user)
                {
                    case UserType.Administrator: return "administrator";
                    case UserType.Programmer: return "programista";
                    case UserType.Seller: return "sprzedawca";
                    case UserType.Warehouseman: return "magazynier";
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
