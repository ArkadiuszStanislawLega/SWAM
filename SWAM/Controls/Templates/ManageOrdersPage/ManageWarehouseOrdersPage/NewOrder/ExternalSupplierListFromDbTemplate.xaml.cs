using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses;
using SWAM.Models.ExternalSupplier;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder
{
    /// <summary>
    /// Interaction logic for ExternalSupplierListFromDbTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierListFromDbTemplate : UserControl
    {
        #region Constructor
        public ExternalSupplierListFromDbTemplate()
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
            // External suppliers list refresh after each page load
            Models.ViewModels.CreateNewWarehouseOrder.ExternalSupplierListViewModel.Instance.Refresh();
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(externalSuppliersListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = ExternalSupplierFilter;
        }
        #endregion

        #region ExternalSupplierFilter
        /// <summary>
        /// Check if external supplier's name contains searching text
        /// </summary>
        /// <param name="item">external supplier object</param>
        /// <returns>True if does</returns>
        private bool ExternalSupplierFilter(object item)
        {
            if (item is ExternalSupplier externalSupplier)
            {
                return (externalSupplier.Name.IndexOf(externalSupplierFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region ExternalSupplier_TextChanged
        /// <summary>
        /// Recreate external supplier list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExternalSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(externalSuppliersListView.ItemsSource).Refresh();
        }
        #endregion

        #region ExternalSupplierViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewWarehouseOrderTemplate) control DataContext with clicked customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExternalSupplierViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is ExternalSupplier externalSupplier)
            {
                if (externalSupplier == null)
                    return;

                if (SWAM.MainWindow.FindParent<CreateNewWarehouseOrderTemplate>(this) is CreateNewWarehouseOrderTemplate parent)
                {
                    parent.externalSupplierProfile.DataContext = externalSupplier;
                }
            }
        }
        #endregion
    }
}
