using SWAM.Enumerators;
using SWAM.Strings;
using System;
using System.Globalization;

namespace SWAM.Converters
{
	public class ENtoPLpaymentStatus : BaseValueConverter<ENtoPLpaymentStatus>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is PaymentStatus paymentType)
			{
				switch (paymentType)
				{
					case PaymentStatus.AwaitingPayment: return PLstrings.AwaitingPayment;
					case PaymentStatus.Paid: return PLstrings.Paid;

					default: return paymentType.ToString();
				}
			}

			else if (value is bool)
			{
				switch (value)
				{
					case false: return PLstrings.AwaitingPayment;
					case true: return PLstrings.Paid;

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
