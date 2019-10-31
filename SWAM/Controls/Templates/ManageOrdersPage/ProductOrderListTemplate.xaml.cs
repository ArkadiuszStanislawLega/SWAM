using SWAM.Models.Customer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text.RegularExpressions;
using SWAM.Models.ViewModels.CreateNewCustomerOrder;
using SWAM.Models.ProductOrderList;

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
                    quantity.Text = state.Available.ToString();
                    customerOrderPosition.Quantity = state.Available;
                }
            }

            PaymentOrderViewModel.Instance.Refresh();
        }

        /// <summary>
        /// Make quantity textbox field accept numeric input type only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (!(((FrameworkElement)sender).DataContext is CustomerOrderPosition customerOrderPosition))
                return;

            ProductOrderListViewModel.Instance.Delete(customerOrderPosition.Id);
            PaymentOrderViewModel.Instance.Refresh();
        }
    }
}
