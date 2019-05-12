using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
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
        /// When the product is oredered but isn't in ship.
        /// </summary>
        Ordered
    }
}
