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
    /// Converter that checks whether the account is blocked, depending on that gives the text(content) in block/unblock button which is written in user profile.
    /// </summary>
    public class AccountBlockToTextConverter : BaseValueConverter<AccountBlockToTextConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusOfUserAccount status && status == StatusOfUserAccount.Blocked)
                return "Odblokuj";

            return "Zablokuj";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
