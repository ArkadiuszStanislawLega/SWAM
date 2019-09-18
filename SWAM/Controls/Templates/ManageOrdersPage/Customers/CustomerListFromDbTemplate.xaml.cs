﻿using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        /// <summary>
        /// List containing all customers from database
        /// </summary>
        List<Customer> _customers = new List<Customer>();
        #endregion

        #region Basic Constructor
        public CustomerListFromDbTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region GetCustomersFromDb
        private List<Customer> GetCustomersFromDb() { return _context.Customers.Include(c => c.ResidentAddress).Include(c => c.Phones).ToList(); }
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
                    parent.DataContext = customer;
                }
            }
        }
        #endregion
    }
}
