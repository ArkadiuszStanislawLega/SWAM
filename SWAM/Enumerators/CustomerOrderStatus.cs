
namespace SWAM.Enumerators
{
    /// <summary>
    /// Stores the status of the order to the customer.
    /// </summary>
    public enum CustomerOrderStatus
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
        /// When product is not sent yet.
        /// </summary>
        InProcess
    }
}
