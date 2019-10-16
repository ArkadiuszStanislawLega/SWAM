﻿using System;
using System.Collections.Generic;
using SWAM.Enumerators;

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
        /// The employee who is responsible for the order.
        /// </summary>
        public User.User User { get; set; }
        /// <summary>
        /// Foreign key property
        /// </summary>
        public int UserId { get; set; }
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
        public IList<CustomerOrderPosition> CustomerOrderPositions { get; set; }
    }
}
