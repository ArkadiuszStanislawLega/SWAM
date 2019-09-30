using System;
using System.Collections.Generic;
using SWAM.Enumerators;

namespace SWAM.Models.Warehouse
{
    /// <summary>
    /// The basic model of the class in the database representing the order of <see cref="Product"/>s to the <see cref="SWAM.Models.Warehouse"/>.
    /// </summary>
    public class WarehouseOrder
    {
        /// <summary>
        /// Warehouse order identification number in the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of the order.
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Date of delivery of products from the order to the warehouse.
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// The warehouse to which the products were ordered.
        /// </summary>
        public Warehouse Warehouse { get; set; }
        /// <summary>
        /// An external supplier who delivers products from the order.
        /// </summary>
        public ExternalSupplier ExternalSupplayer { get; set; }
        /// <summary>
        /// Current order status.
        /// </summary>
        public WarehouseOrderStatus WarehouseOrderStatus { get; set; }
        /// <summary>
        /// All items with products from the order.
        /// </summary>
        public IList<WarehouseOrderPosition> OrderPositions { get; set; }
    }
}
