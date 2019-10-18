using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    /// <summary>
    /// Translate Shipment types to Polish.
    /// </summary>
    public class ENtoPLshipmentTypeConverter : BaseValueConverter<ENtoPLshipmentTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShipmentType shipmnetType)
            {
                switch (shipmnetType)
                {
                    case ShipmentType.Reception: return PLstrings.RECEPTION;
                    case ShipmentType.Shipment: return PLstrings.SHIPMENT;
                   
                    default: return shipmnetType.ToString();
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
