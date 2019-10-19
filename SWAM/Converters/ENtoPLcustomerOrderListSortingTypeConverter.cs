using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class ENtoPLcustomerOrderListSortingTypeConverter : BaseValueConverter<ENtoPLcustomerOrderListSortingTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomerOrdersListSortingType customerOrderStatus)
            {
                switch (customerOrderStatus)
                {
                    case CustomerOrdersListSortingType.Id: return PLstrings.ID;
                    case CustomerOrdersListSortingType.OrderDate: return PLstrings.ORDER_DATE;
                    case CustomerOrdersListSortingType.OrderStatus: return PLstrings.ORDER_STATUS;

                    default: return customerOrderStatus.ToString();
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
