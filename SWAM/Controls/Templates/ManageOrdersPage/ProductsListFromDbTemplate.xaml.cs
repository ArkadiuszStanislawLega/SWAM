﻿using SWAM.Models;
using SWAM.Models.Customer;
using SWAM.Models.ManageOrdersPage;
using SWAM.Models.ProductOrderList;
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

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void AddToShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            if (!(((FrameworkElement)sender).DataContext is Product product))
                return;

            var customerOrderPosition = new CustomerOrderPosition
            {
                Product = product,
                Price = product.Price
            };

            ProductOrderListViewModel.Instance.Add(customerOrderPosition);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) => UserDependsAccessProductListViewModel.Instance.Refresh();
        
    }
}
