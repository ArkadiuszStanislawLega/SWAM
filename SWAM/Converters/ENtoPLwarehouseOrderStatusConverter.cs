using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class ENtoPLwarehouseOrderStatusConverter : BaseValueConverter<ENtoPLwarehouseOrderStatusConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WarehouseOrderStatus warehouseOrderStatus)
            {
                switch (warehouseOrderStatus)
                {
                    case WarehouseOrderStatus.Delivered: return PLstrings.DELIVERED;
                    case WarehouseOrderStatus.InDelivery: return PLstrings.IN_DELIVERY;
                    case WarehouseOrderStatus.Ordered: return PLstrings.ORDERED;

                    default: return warehouseOrderStatus.ToString();
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
