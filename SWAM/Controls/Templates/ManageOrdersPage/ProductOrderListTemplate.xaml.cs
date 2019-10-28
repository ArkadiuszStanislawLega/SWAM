﻿using SWAM.Models.Customer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for ProductOrderListTemplate.xaml
    /// </summary>
    public partial class ProductOrderListTemplate : UserControl
    {
        public ProductOrderListTemplate()
        {
            InitializeComponent();
        }

        private void ProductFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Checks if quantity is not greater than available products number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            var quantity = sender as TextBox;
            var customerOrderPosition = quantity.DataContext as CustomerOrderPosition;

            if (quantity.Text == String.Empty)
                return;

            foreach (var state in customerOrderPosition.Product.States)
            {
                if(int.Parse(quantity.Text) > state.Quantity)
                {
                    quantity.Text = state.Quantity.ToString();
                }
            } 
        }
    }
}
