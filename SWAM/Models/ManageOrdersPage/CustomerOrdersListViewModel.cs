using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SWAM.Enumerators;
using System;
using SWAM.Strings;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace SWAM.Models.ManageOrdersPage
{
    public class CustomerOrdersListViewModel : UserControl
    {
        private readonly ObservableCollection<CustomerOrder> _customerOrders = new ObservableCollection<CustomerOrder>();
        public ObservableCollection<CustomerOrder> CustomerOrders => this._customerOrders;

        /// <summary>
        /// The type of status of delivery.
        /// </summary>
        public CustomerOrderStatus CustomerOrderStatus { get; set; }

        public int Id { get; set; }

        #region Database connection
        private static ApplicationDbContext dbContext = new ApplicationDbContext();
        private static ApplicationDbContext context
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
        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CustomerOrdersListViewModel() { _instance.Refresh(); }

        private static readonly CustomerOrdersListViewModel _instance = new CustomerOrdersListViewModel();
        public static CustomerOrdersListViewModel Instance => _instance;
        public CustomerOrdersListViewModel() { }
        #endregion

        public void Refresh()
        {
            if (CustomerOrder.GetAllOrders() is IList<CustomerOrder> list)
            {
                if (this._customerOrders.Count > 0)
                    this._customerOrders.Clear();

                foreach (var item in list)
                {
                    this._customerOrders.Add(item);
                }
            }
        }

        #region ChangeStatus
        /// <summary>
        /// Changing status in database.
        /// </summary>
        /// <param name="customerOrderStatus">New status.</param>
        public void ChangeStatus(CustomerOrderStatus customerOrderStatus)
        {
            context.CustomerOrders.OfType<CustomerOrder>().FirstOrDefault(u => u.Id == this.Id).CustomerOrderStatus = customerOrderStatus;
            context.SaveChanges();
        }
        #endregion

        public static List<CustomerOrder> AllCustomerOrdersList() => context
        .CustomerOrders.OfType<CustomerOrder>()
        .ToList();
    }
}
