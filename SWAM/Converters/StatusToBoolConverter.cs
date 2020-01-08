using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Converters
{
	class StatusToBoolConverter : BaseValueConverter<StatusToBoolConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is WarehouseOrderStatus warehouseOrderStatus)
			{
				switch (warehouseOrderStatus)
				{
					case WarehouseOrderStatus.Delivered: return false;
					case WarehouseOrderStatus.InDelivery: return true;
					case WarehouseOrderStatus.Ordered: return true;

					default: return true;
				}
			}
			else return true;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
