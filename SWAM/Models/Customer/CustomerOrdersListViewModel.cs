using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace SWAM.Models.Customer
{
    public class CustomerOrdersListViewModel : UserControl
    {
        private static ObservableCollection<CustomerOrder> _orders = new ObservableCollection<CustomerOrder>();
        public static ObservableCollection<CustomerOrder> Orders => _orders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CustomerOrdersListViewModel() { }

        private static readonly CustomerOrdersListViewModel _instance = new CustomerOrdersListViewModel();
        public static CustomerOrdersListViewModel Instance => _instance;
        #endregion

        public void Refresh(Customer customer)
        {
            if (customer != null)
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var customers = context.People
                        .OfType<Customer>()
                        .Include(c => c.Orders)
                        .FirstOrDefault(c => c.Id == customer.Id);

                    if (customers != null && _orders.Count > 0)
                        _orders.Clear();

                    foreach (var order in customers.Orders)
                    {
                        _orders.Add(order);
                    }
                }
            }
        }
    }
}
