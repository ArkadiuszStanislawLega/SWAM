using SWAM.Models;
using SWAM.Models.Customer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Interaction logic for CustomerListFromDbTemplate.xaml
    /// </summary>
    public partial class CustomerListFromDbTemplate : UserControl
    {
        #region Properties
        ApplicationDbContext _context = new ApplicationDbContext();
        #endregion

        #region Basic Constructor
        public CustomerListFromDbTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region Window_Loaded
        /// <summary>
        /// Provide initial configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Customer list refresh after each page load
            CustomersListViewModel.Instance.Refresh();
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(customersListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = CustomerFilter;
        }
        #endregion

        #region CustomerFilter
        /// <summary>
        /// Check if customer's full name contains searching text
        /// </summary>
        /// <param name="item">customer object</param>
        /// <returns>True if does</returns>
        private bool CustomerFilter(object item)
        {
            if (item is Customer customer)
            {
                string fullName = customer.Name + " " + customer.Surname;
                return (fullName.IndexOf(customerFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region CustomerFilter_TextChanged
        /// <summary>
        /// Recreate customers list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(customersListView.ItemsSource).Refresh();
        }
        #endregion

        #region CustomersListViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewCustomerOrderTemplate) control DataContext with clicked customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomersListViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is Customer customer)
            {
                if (customer == null)
                    return;

                if (SWAM.MainWindow.FindParent<CreateNewCustomerOrderTemplate>(this) is CreateNewCustomerOrderTemplate parent)
                {
                    parent.customerProfile.DataContext = customer;
                }
            }
        }
        #endregion
    }
}
