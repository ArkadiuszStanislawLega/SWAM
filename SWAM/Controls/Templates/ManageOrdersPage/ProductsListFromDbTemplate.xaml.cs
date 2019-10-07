using SWAM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for ProductsListFromDbTemplate.xaml
    /// </summary>
    public partial class ProductsListFromDbTemplate : UserControl
    {
        public ProductsListFromDbTemplate()
        {
            InitializeComponent();
        }

        private List<Product> GetProductListFromUserWarehouse()
        {
            var accessUsersToWarehouses = AccessUsersToWarehouses.GetUserAccess(SWAM.MainWindow.LoggedInUser.Id);
            if (accessUsersToWarehouses != null)
            {
                return Product.GetProductsFromWarehouse(accessUsersToWarehouses.User.Id);
            }
            return null;
        }

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void AddToShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("adsa");
        }
    }
}
