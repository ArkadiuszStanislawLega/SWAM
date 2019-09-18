using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.ManageOrdersPage
{
    /// <summary>
    /// Singleton customers list view model, holds all customers from database.
    /// </summary>
    public class CustomersListViewModel : UserControl
    {
        #region Properties
        /// <summary>
        /// Customers list view model, holds all customers from database.
        /// </summary>
        private static readonly ObservableCollection<Customer> _customersList = new ObservableCollection<Customer>();
        /// <summary>
        /// Customers list view model, holds all customers from database.
        /// </summary>
        public static ObservableCollection<Customer> CustomersList => _customersList;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CustomersListViewModel() => _instance.RefreshList();

        private static readonly CustomersListViewModel _instance = new CustomersListViewModel();
        public static CustomersListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void RefreshList()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var customersList = context.Customers.Include(c => c.ResidentAddress).Include(c => c.Phones).ToList();

                if (customersList != null && _customersList.Count > 0)
                    _customersList.Clear();

                foreach (var customer in customersList)
                {
                    _customersList.Add(customer);
                }
            }
        }
        #endregion
    }
}
