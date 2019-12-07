using System;
using System.Collections.Generic;
using SWAM.Enumerators;
using System.Data.Entity;
using System.Linq;
using SWAM.Strings;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Windows;

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
        #region Database connection
        private static ApplicationDbContext dbContext = new ApplicationDbContext();
        private static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return dbContext;
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
            set => dbContext = value;
        }
        #endregion
        public static IList<WarehouseOrder> GetAllOrders()
        {
            return Context.WarehouseOrders
                 .Include(w => w.ExternalSupplayer)
                 .Include(w => w.OrderPositions)
                 .Include(w => w.Warehouse)
				 .Include(w => w.Creator)
				 .Include(w => w.UserReceivedOrder)
				 .ToList();
        }

		public static void ChangePaymentStatus(PaymentStatus status, WarehouseOrder order)
		{
			if (status.ToString() != null)
			{				
				var dbOrder = Context.WarehouseOrders.FirstOrDefault(p => p.Id == order.Id);
				if (status == PaymentStatus.Paid) dbOrder.IsPaid = true;
				else dbOrder.IsPaid = false;
				Context.SaveChanges();				
			}			
		}

		public static void ChangeDeliveryStatus(WarehouseOrderStatus status, WarehouseOrder order)
		{
			var dbOrder = Context.WarehouseOrders.FirstOrDefault(p => p.Id == order.Id);
			if (status == WarehouseOrderStatus.Ordered) dbOrder.WarehouseOrderStatus = WarehouseOrderStatus.Ordered;
			else if (status == WarehouseOrderStatus.InDelivery) dbOrder.WarehouseOrderStatus = WarehouseOrderStatus.InDelivery;
			else if (status == WarehouseOrderStatus.Delivered)
			{
				dbOrder.WarehouseOrderStatus = WarehouseOrderStatus.Delivered;
				dbOrder.DeliveryDate = DateTime.Now;
				dbOrder.UserReceivedOrderId = MainWindow.LoggedInUser.Id;				
			}
			Context.SaveChanges();			
		}
		
	}
}
