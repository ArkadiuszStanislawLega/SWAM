using System;
using System.Collections.Generic;
using System.Globalization;
using SWAM.Enumerators;

namespace SWAM.Converters
{
    using static SWAM.MainWindow;

    public class PagesUserControlsToBoolean : BaseValueConverter<PagesUserControlsToBoolean>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pageNavigation = (PagesUserControls)value;
            bool pageCanBeOpen = false;
            if (IsLoggedIn)
            {
                if (PAGES_FOR_USER.TryGetValue(LoggedInUser.Permissions, out List<PagesUserControls> listWithPermissions))
                {
                    foreach (PagesUserControls puc in listWithPermissions)
                    {
                        if (puc == pageNavigation)
                        {
                            pageCanBeOpen = true;
                            break;
                        }
                    }
                }
            }
            return pageCanBeOpen;
        }
    

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
