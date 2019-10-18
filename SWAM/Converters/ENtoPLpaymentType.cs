using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
    public class ENtoPLpaymentType : BaseValueConverter<ENtoPLpaymentType>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PaymentType paymentType)
            {
                switch (paymentType)
                {
                    case PaymentType.Prepaid: return PLstrings.PREPAID;
                    case PaymentType.Postpaid: return PLstrings.POSTPAID;
                   
                    default: return paymentType.ToString();
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
