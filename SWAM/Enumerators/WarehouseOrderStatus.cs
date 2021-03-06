﻿
namespace SWAM.Enumerators
{
    /// <summary>
    /// Stores the status of the order to the warehouses.
    /// </summary>
    public enum WarehouseOrderStatus
    {
        /// <summary>
        /// When the order was delivered to the warehouse.
        /// This is the last status of order.
        /// </summary>
        Delivered,
        /// <summary>
        /// When the product is ordered but it's not delivered yet to warehouse.
        /// Product is in ship.
        /// </summary>
        InDelivery,
        /// <summary>
        /// When product is oredered but is not sent yet.
        /// </summary>
        Ordered
    }
}
