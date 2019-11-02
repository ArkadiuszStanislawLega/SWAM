using SWAM.Models;
using SWAM.Models.ManageOrdersPage;
using SWAM.Models.ViewModels.CreateNewCustomerOrder;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
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

namespace SWAM.Controls.Templates.ManageOrdersPage.NewOrder
{
    /// <summary>
    /// Interaction logic for WarehouseAccessListTemplate.xaml
    /// </summary>
    public partial class WarehouseAccessListTemplate : UserControl
    {
        #region Constructor
        public WarehouseAccessListTemplate()
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
            UserDependsAccessProductListViewModel.Instance.Refresh();
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(warehouseListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = WarehouseFilter;
        }
        #endregion

        #region WarehouseFilter
        /// <summary>
        /// Check if warehouse name contains searching text
        /// </summary>
        /// <param name="item">customer object</param>
        /// <returns>True if does</returns>
        private bool WarehouseFilter(object item)
        {
            if (item is Warehouse warehouse)
            {
                return (warehouse.Name.IndexOf(warehouseFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region WarehouseFilter_TextChanged
        /// <summary>
        /// Recreate warehouse list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(warehouseListView.ItemsSource).Refresh();
        }
        #endregion

        #region WarehouseListViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewCustomerOrderTemplate) control DataContext with clicked customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseListViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is Warehouse warehouse)
            {
                if (warehouse == null)
                    return;

                UserDependsAccessProductListViewModel.Instance.SetStates(warehouse);
                WarehouseInformationViewModel.Instance.SetWarehouse(warehouse);
            }
        }
        #endregion
    }
}
