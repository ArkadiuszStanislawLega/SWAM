using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class ENtoPLcustomerOrderStatus : BaseValueConverter<ENtoPLcustomerOrderStatus>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomerOrderStatus user)
            {
                switch (user)
                {
                    case CustomerOrderStatus.Delivered: return PLstrings.DELIVERED;
                    case CustomerOrderStatus.InDelivery: return PLstrings.IN_DELIVERY;
                    case CustomerOrderStatus.InProcess: return PLstrings.IN_PROCESS;
                    case CustomerOrderStatus.WaitingForPayment: return PLstrings.WAITING_FOR_PAYMENT;

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
