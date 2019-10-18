
using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class ENtoPLshipmentTypeConverter : BaseValueConverter<ENtoPLshipmentTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShipmentType user)
            {
                switch (user)
                {
                    case ShipmentType.Reception: return PLstrings.RECEPTION;
                    case ShipmentType.Shipment: return PLstrings.SHIPMENT;
                   
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
