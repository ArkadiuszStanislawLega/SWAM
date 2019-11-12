using System;
using System.Collections.Generic;
using SWAM.Enumerators;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.Customer
{
    /// <summary>
    /// The basic model of the class in the database representing the <see cref="Customer"/>'s order.
    /// </summary>
    public class CustomerOrder
    {
        /// <summary>
        /// Customer order numer Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Indicates if order has been paid
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// Date when order was made
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Date when products were delivered to customer
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// The warehouse from which the product was released.
        /// </summary>
        public Warehouse.Warehouse Warehouse { get; set; }
        /// <summary>
        /// Foreign key property
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        /// The employee who created order.
        /// </summary>
        public User.User Creator { get; set; }
        /// <summary>
        /// Foreign key property
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// The customer who made the purchase.
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// Foreign key property
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// The courier who is responsible for delivering the product.
        /// </summary>
        public Courier.Courier Courier { get; set; }
        /// <summary>
        /// Foreign key property
        /// </summary>
        public int? CourierId { get; set; }
        /// <summary>
        /// Status of the order to the customer.
        /// </summary>
        public CustomerOrderStatus CustomerOrderStatus { get; set; }
        /// <summary>
        /// The type of delivery of the product to the customer.
        /// </summary>
        public ShipmentType ShipmentType { get; set; }
        /// <summary>
        /// Payment type chosen by the customer.
        /// </summary>
        public PaymentType PaymentType { get; set; }
        /// <summary>
        /// Address to which the goods will be delivered to the customer.
        /// </summary>
        public CustomerOrderDeliveryAddress DeliveryAddress { get; set; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get; set; }

        //TODO:TRy catch block.
        private static ApplicationDbContext context = new ApplicationDbContext();
        public static IList<CustomerOrder> GetAllOrders()
        {
           return context.CustomerOrders
                .Include(c => c.Customer)
                .Include(c => c.Courier)
                .Include(c => c.Warehouse)
                .Include(c => c.Creator)
                .Include(c => c.DeliveryAddress)
                .ToList();
        }


    }
}
