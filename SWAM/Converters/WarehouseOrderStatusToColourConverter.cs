using SWAM.Enumerators;
using System;
using System.Globalization;
using System.Windows.Media;

namespace SWAM.Converters
{
    public class WarehouseOrderStatusToColourConverter : BaseValueConverter<WarehouseOrderStatusToColourConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WarehouseOrderStatus warehouseOrderStatus)
            {
                switch (warehouseOrderStatus)
                {
                    case WarehouseOrderStatus.Delivered: return (Color)SWAM.MainWindow.currentInstance.FindResource("DarkGraphite");
                    case WarehouseOrderStatus.InDelivery: return (Color)SWAM.MainWindow.currentInstance.FindResource("LightBlue");
                    case WarehouseOrderStatus.Ordered: return (Color)SWAM.MainWindow.currentInstance.FindResource("WhiteCream");
                    default: return value.ToString();
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
