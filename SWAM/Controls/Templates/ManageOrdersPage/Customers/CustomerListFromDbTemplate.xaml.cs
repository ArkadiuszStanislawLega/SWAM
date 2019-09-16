using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Interaction logic for CustomerListFromDbTemplate.xaml
    /// </summary>
    public partial class CustomerListFromDbTemplate : UserControl
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private List<Customer> _customers = new List<Customer>();

        public CustomerListFromDbTemplate()
        {
            InitializeComponent();
            _customers = GetCustomersFromDb();

            lvUsers.ItemsSource = _customers;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            view.Filter = UserFilter;
            
        }

        private List<Customer> GetCustomersFromDb() { return _context.Customers.ToList(); }

        private void CreateCustomersData()
        {
            var customerList = GetCustomersFromDb();

            foreach (var customer in customerList)
            {
                _customers.Add(customer);
            }
        }

        private bool UserFilter(object item)
        {
            Customer customer = item as Customer;
            if (customer != null)
            {
                string CombinedName = customer.Name + " " + customer.Surname;
                return (CombinedName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }
}
