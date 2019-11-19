using System;
using System.Collections.Generic;
using SWAM.Enumerators;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using SWAM.Strings;

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

        #region Database connection
        private static ApplicationDbContext context = new ApplicationDbContext();
        private static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return context;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => context = value;
        }
        #endregion
        public static IList<CustomerOrder> GetAllOrders()
        {
           return Context.CustomerOrders
                .Include(c => c.Customer)
                .Include(c => c.Courier)
                .Include(c => c.Warehouse)
                .Include(c => c.Creator)
                .Include(c => c.DeliveryAddress)
                .ToList();
        }
        #region Get
        /// <summary>
        /// Resived customer order from database.
        /// </summary>
        /// <param name="id">ID number of the order we want to download.</param>
        /// <returns>Full customer order with, customer, creater, courier and warehouse.</returns>
        public static CustomerOrder Get(int id)
        {
            Context = new ApplicationDbContext();
            return Context.CustomerOrders
                .Include(c => c.Customer)
                .Include(c => c.Creator)
                .Include(c => c.Courier)
                .Include(c => c.Warehouse)
                .FirstOrDefault(c => c.Id == id);
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edit customer order properties.
        /// </summary>
        /// <param name="customerOrder">New customer order values.</param>
        public void Edit(CustomerOrder customerOrder)
        {
            if(customerOrder != null)
            {
                Context = new ApplicationDbContext();
                var customerOrderDb = Context.CustomerOrders.FirstOrDefault(c => c.Id == this.Id);
                customerOrderDb.PaymentType = customerOrder.PaymentType;
                customerOrderDb.ShipmentType = customerOrder.ShipmentType;
                if(customerOrder != null)
                    customerOrderDb.DeliveryAddress = customerOrder.DeliveryAddress;

                if (customerOrder.Courier != null)
                    customerOrderDb.Courier = customerOrder.Courier;

                Context.SaveChanges();
            }
        }
        #endregion
    }
}
