using SWAM.Models.ViewModels.CreateNewWarehouseOrder;
using SWAM.Models.Warehouse;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder
{
    /// <summary>
    /// Interaction logic for ProductOrderListTemplate.xaml
    /// </summary>
    public partial class ProductOrderListTemplate : UserControl
    {
        #region Constructor
        public ProductOrderListTemplate()
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
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(warehouseOrderPositionsListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = WarehouseOrderPositionFilter;
        }
        #endregion

        #region WarehouseOrderPositionFilter
        /// <summary>
        /// Check if product's name contains searching text
        /// </summary>
        /// <param name="item">warehouse order position object</param>
        /// <returns>True if does</returns>
        private bool WarehouseOrderPositionFilter(object item)
        {
            if (item is WarehouseOrderPosition warehouseOrderPosition)
            {
                return (warehouseOrderPosition.Product.Name.IndexOf(warehouseOrderPositionFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region WarehouseOrderPositionFilter_TextChanged
        /// <summary>
        /// Recreate warehouse order positions list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseOrderPositionFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(warehouseOrderPositionsListView.ItemsSource).Refresh();
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
            if (!(((FrameworkElement)sender).DataContext is WarehouseOrderPosition warehouseOrderPosition))
                return;

            ProductOrderListViewModel.Instance.Delete(warehouseOrderPosition.ProductId);
            //PaymentOrderViewModel.Instance.Refresh();
        }
        #endregion
    }
}
