using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace SWAM.Models.Customer
{
    public class CustomerOrdersListViewModel : UserControl
    {
        private readonly ObservableCollection<CustomerOrder> _orders = new ObservableCollection<CustomerOrder>();
        public ObservableCollection<CustomerOrder> Orders => _orders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CustomerOrdersListViewModel() { }

        private static readonly CustomerOrdersListViewModel _instance = new CustomerOrdersListViewModel();
        public static CustomerOrdersListViewModel Instance => _instance;
        public CustomerOrdersListViewModel() { }
        #endregion

        public void Refresh(Customer customer)
        {
            if (customer != null)
            {
                if (_orders.Count > 0)
                    _orders.Clear();

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var orders = context.CustomerOrders
                        .Include(c => c.Courier)
                        .Include(c => c.Warehouse)
                        .Include(c => c.CustomerOrderPositions) 
                        .Include(c => c.DeliveryAddress)
                        .Where(c => c.Customer.Id == customer.Id)
                        .ToList();

                    foreach( var order in orders)
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
