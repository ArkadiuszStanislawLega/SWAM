using SWAM.Enumerators;
using System;
using System.Globalization;
using System.Windows.Media;

namespace SWAM.Converters
{
    public class CustomerOrderStatusToColourConverter : BaseValueConverter<CustomerOrderStatusToColourConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
       
            if (value is CustomerOrderStatus user)
            {
                switch (user)
                {
                    case CustomerOrderStatus.Delivered: return (Color)SWAM.MainWindow.currentInstance.FindResource("DarkGraphite");
                    case CustomerOrderStatus.InDelivery: return (Color)SWAM.MainWindow.currentInstance.FindResource("LightBlue");
                    case CustomerOrderStatus.InProcess: return (Color)SWAM.MainWindow.currentInstance.FindResource("WhiteCream");
                    case CustomerOrderStatus.WaitingForPayment: return (Color)SWAM.MainWindow.currentInstance.FindResource("Red");
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
