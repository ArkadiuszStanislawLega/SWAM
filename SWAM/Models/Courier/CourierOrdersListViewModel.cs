using SWAM.Models.Customer;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Windows.Controls;

namespace SWAM.Models.Courier
{
    public class CourierOrdersListViewModel : UserControl
    {
        private readonly static ObservableCollection<CustomerOrder> _orders = new ObservableCollection<CustomerOrder>();
        public static ObservableCollection<CustomerOrder> Orders => _orders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CourierOrdersListViewModel() { }

        private static readonly CourierOrdersListViewModel _instance = new CourierOrdersListViewModel();
        public static CourierOrdersListViewModel Instance => _instance;
        public CourierOrdersListViewModel() { }
        #endregion

        public void Refresh(Courier courier)
        {
            if (courier != null)
            {
                if (_orders.Count > 0)
                    _orders.Clear();

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
                        order.User = context.People
                            .OfType<User.User>()
                            .Include(u => u.Phones)
                            .FirstOrDefault(u => u.Id == order.UserId);

                        foreach (var position in order.CustomerOrderPositions)
                        {
                            position.Product = context.Products.FirstOrDefault(p => p.Id == position.ProductId);
                        }

                        _orders.Add(order);
                    }
                }
            }
        }
    }
}
