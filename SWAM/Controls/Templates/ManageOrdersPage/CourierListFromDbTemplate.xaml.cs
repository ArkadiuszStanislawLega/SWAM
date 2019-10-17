using SWAM.Controls.Templates.ManageOrdersPage.Customers;
using SWAM.Models.Courier;
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

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for CourierListFromDbTemplate.xaml
    /// </summary>
    public partial class CourierListFromDbTemplate : UserControl
    {
        #region Properties
        ApplicationDbContext _context = new ApplicationDbContext();
        #endregion

        #region Basic Constructor
        public CourierListFromDbTemplate()
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
            CouriersListViewModel.Instance.Refresh();
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(couriersListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = CourierFilter;
        }
        #endregion

        #region CustomerFilter
        /// <summary>
        /// Check if courier's name contains searching text
        /// </summary>
        /// <param name="item">courier object</param>
        /// <returns>True if does</returns>
        private bool CourierFilter(object item)
        {
            if (item is Courier courier)
            {
                return (courier.Name.IndexOf(courierFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region CourierFilter_TextChanged
        /// <summary>
        /// Recreate courier list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CourierFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(couriersListView.ItemsSource).Refresh();
        }
        #endregion

        #region CourierListViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewCustomerOrderTemplate) control DataContext with clicked customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CourierListViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is Courier courier)
            {
                if (courier == null)
                    return;

                if (SWAM.MainWindow.FindParent<CreateNewCustomerOrderTemplate>(this) is CreateNewCustomerOrderTemplate parent)
                {
                    parent.DataContext = courier;
                }
            }
        }
        #endregion
    }
}
