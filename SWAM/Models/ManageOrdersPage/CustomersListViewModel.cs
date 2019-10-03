using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;
using System;

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
        private static readonly ObservableCollection<Customer.Customer> _customersList = new ObservableCollection<Customer.Customer>();
        /// <summary>
        /// Customers list view model, holds all customers from database.
        /// </summary>
        public static ObservableCollection<Customer.Customer> CustomersList => _customersList;
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
                var customers = context.People.OfType<Customer.Customer>().Include(c => c.ResidentalAddress).Include(c => c.Phone).Include(c => c.EmailAddress).ToList();

                if (customers != null && _customersList.Count > 0)
                    _customersList.Clear();

                foreach (var customer in customers)
                {
                    _customersList.Add(customer);
                }
            }
        }
        #endregion
    }
}
