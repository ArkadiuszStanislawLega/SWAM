using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    /// <summary>
    /// Stores the status of the order to the customer.
    /// </summary>
    enum CustomerOrderStatus
    {
        /// <summary>
        /// When the order was delivered to the customer.
        /// This is the last status of order.
        /// </summary>
        Delivered,
        /// <summary>
        /// When the product is ordered but it's not delivered yet to customer.
        /// Product is in ship.
        /// </summary>
        InDelivery,
        /// <summary>
        /// When the customer ordered product but hasn't payed yet.
        /// </summary>
        WaitingForPayment,
        /// <summary>
        /// When the payment is done and product is not sent yet.
        /// </summary>
        InProcess
    }
}
