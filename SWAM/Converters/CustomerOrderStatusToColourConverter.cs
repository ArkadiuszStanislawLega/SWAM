using SWAM.Enumerators;
using System;
using System.Globalization;
using System.Windows.Media;

namespace SWAM.Converters
{
    /// <summary>
    /// Change customer order status to specific color in customer order list view model tempalte.
    /// </summary>
    public class CustomerOrderStatusToColourConverter : BaseValueConverter<CustomerOrderStatusToColourConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomerOrderStatus user)
            {
                switch (user)
                {
                    case CustomerOrderStatus.Delivered: return (Color)SWAM.MainWindow.Instance.FindResource("DarkGraphite");
                    case CustomerOrderStatus.InDelivery: return (Color)SWAM.MainWindow.Instance.FindResource("LightBlue");
                    case CustomerOrderStatus.InProcess: return (Color)SWAM.MainWindow.Instance.FindResource("WhiteCream");
                    case CustomerOrderStatus.WaitingForPayment: return (Color)SWAM.MainWindow.Instance.FindResource("Red");
                    default : return value.ToString();
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
