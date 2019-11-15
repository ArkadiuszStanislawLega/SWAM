using SWAM.Models.Customer;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Windows.Controls;
using System;
using SWAM.Strings;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace SWAM.Models.Courier
{
    public class CourierOrdersListViewModel : UserControl
    {
        private readonly ObservableCollection<CustomerOrder> _orders = new ObservableCollection<CustomerOrder>();
        public ObservableCollection<CustomerOrder> Orders => _orders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CourierOrdersListViewModel() { }

        private static readonly CourierOrdersListViewModel _instance = new CourierOrdersListViewModel();
        public static CourierOrdersListViewModel Instance => _instance;
        public CourierOrdersListViewModel() { }
        #endregion
        #region Refresh
        /// <summary>
        /// Geting courier orders from database.
        /// </summary>
        /// <param name="courier">The courier whose orders are to be retrieved.</param>
        public void Refresh(Courier courier)
        {
            if (courier != null)
            {
                if (_orders.Count > 0)
                    _orders.Clear();

                try
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        var orders = context.CustomerOrders
                            .Include(c => c.Customer)
                            .Include(c => c.Courier)
                            .Include(c => c.Warehouse)
                            .Include(c => c.CustomerOrderPositions)
                            .Include(c => c.DeliveryAddress)
                            .Where(c => c.Courier.Id == courier.Id)
                            .ToList();

                        foreach (var order in orders)
                        {
                            order.Creator = context.People
                                .OfType<User.User>()
                                .Include(u => u.Phones)
                                .FirstOrDefault(u => u.Id == order.CreatorId);

                            foreach (var position in order.CustomerOrderPositions)
                            {
                                position.Product = context.Products.FirstOrDefault(p => p.Id == position.ProductId);
                            }

                            _orders.Add(order);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }

            }
        }
        #endregion
    }
}
