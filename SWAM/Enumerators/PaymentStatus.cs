using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Enumerators
{
	/// <summary>
	/// Payment status
	/// </summary>
	public enum PaymentStatus
	{
		/// <summary>
		/// Order is paid by customer
		/// </summary>
		Paid,
		/// <summary>
		/// Awaiting payment for order
		/// </summary>
		AwaitingPayment
	}
}
