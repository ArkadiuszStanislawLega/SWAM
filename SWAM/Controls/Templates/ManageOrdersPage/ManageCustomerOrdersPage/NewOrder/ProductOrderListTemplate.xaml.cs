using SWAM.Models.Customer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text.RegularExpressions;
using SWAM.Models.ViewModels.CreateNewCustomerOrder;
using SWAM.Models.ProductOrderList;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder
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

        #region Window_Loaded
        /// <summary>
        /// Provide initial configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(customerOrderPositionsListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = CustomerOrderPositionFilter;
        }
        #endregion

        #region CustomerOrderPositionFilter
        /// <summary>
        /// Check if product's name contains searching text
        /// </summary>
        /// <param name="item">customer order position object</param>
        /// <returns>True if does</returns>
        private bool CustomerOrderPositionFilter(object item)
        {
            if (item is CustomerOrderPosition customerOrderPosition)
            {
                return (customerOrderPosition.Product.Name.IndexOf(customerOrderPositionFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region CustomerOrderPositionFilter_TextChanged
        /// <summary>
        /// Recreate customer order positions list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerOrderPositionFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(customerOrderPositionsListView.ItemsSource).Refresh();
        }
        #endregion

        #region Quantity_LostFocus
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

            if (int.Parse(quantity.Text) > customerOrderPosition.State.Available)
            {
                quantity.Text = customerOrderPosition.State.Available.ToString();
                customerOrderPosition.Quantity = customerOrderPosition.State.Available;
            }

            PaymentOrderViewModel.Instance.Refresh();
        }
        #endregion

        #region NumberValidationTextBox
        /// <summary>
        /// Make quantity textbox field accept numeric input type only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox quanity = sender as TextBox;
            Regex regex = new Regex("[^1-9]+");

            if (quanity.Text.Length > 0)
                regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion

        #region DeleteRow_Click
        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (!(((FrameworkElement)sender).DataContext is CustomerOrderPosition customerOrderPosition))
                return;

            ProductOrderListViewModel.Instance.Delete(customerOrderPosition.ProductId);
            PaymentOrderViewModel.Instance.Refresh();
        }
        #endregion
    }
}
