using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;
using System.Windows;

namespace SWAM.Converters
{
    /// <summary>
    /// Translate customer order status to Polish.
    /// </summary>
    public class ENtoPLcustomerOrderStatus : BaseValueConverter<ENtoPLcustomerOrderStatus>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomerOrderStatus customerOrderStatus)
            {
                switch (customerOrderStatus)
                {
                    case CustomerOrderStatus.Delivered: return PLstrings.DELIVERED;
                    case CustomerOrderStatus.InDelivery: return PLstrings.IN_DELIVERY;
                    case CustomerOrderStatus.InProcess: return PLstrings.IN_PROCESS;
                    case CustomerOrderStatus.WaitingForPayment: return PLstrings.WAITING_FOR_PAYMENT;

                    default: return customerOrderStatus.ToString();
                }
            }
            else return value.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
