using System;
using System.Collections.Generic;
using SWAM.Enumerators;
using System.Data.Entity;
using System.Linq;

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
        /// Indicates if order has been paid
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// Date of the order.
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// Date of delivery of products from the order to the warehouse.
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// The warehouse to which the products were ordered.
        /// </summary>
        public Warehouse Warehouse { get; set; }
        /// <summary>
        /// Warehouse foreign key property
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        /// The employee who created order.
        /// </summary>
        public User.User Creator { get; set; }
        /// <summary>
        /// User foreign key property
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// The person who receives the order from the supplier in the warehouse.
        /// </summary>
        public User.User UserReceivedOrder { get; set; }
        /// <summary>
        /// UserReceivedOrder foreign key property
        /// </summary>
        public int? UserReceivedOrderId { get; set; }
        /// <summary>
        /// An external supplier who delivers products from the order.
        /// </summary>
        public ExternalSupplier.ExternalSupplier ExternalSupplayer { get; set; }
        /// <summary>
        /// ExternalSupplayer foreign key property
        /// </summary>
        public int ExternalSupplayerId { get; set; }
        /// <summary>
        /// Current order status.
        /// </summary>
        public WarehouseOrderStatus WarehouseOrderStatus { get; set; }
        /// <summary>
        /// All items with products from the order.
        /// </summary>
        public IList<WarehouseOrderPosition> OrderPositions { get; set; }

        //TODO:TRy catch block.
        private static ApplicationDbContext context = new ApplicationDbContext();
        public static IList<WarehouseOrder> GetAllOrders()
        {
            return context.WarehouseOrders
                 .Include(w => w.ExternalSupplayer)
                 .Include(w => w.OrderPositions)
                 .Include(w => w.Warehouse)                
                 .ToList();
        }
    }
}
