using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses;
using SWAM.Models.ExternalSupplier;
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

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage
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
                return (externalSupplier.Name.IndexOf(customerFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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

        #region CustomersListViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewWarehouseOrderTemplate) control DataContext with clicked customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExternalSupplierViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is ExternalSupplier    externalSupplier)
            {
                if (externalSupplier == null)
                    return;

                if (SWAM.MainWindow.FindParent<CreateNewWarehouseOrderTemplate>(this) is CreateNewWarehouseOrderTemplate parent)
                {
                    //parent.externalSupplierProfile.DataContext = externalSupplier;
                }
            }
        }
        #endregion
    }
}
