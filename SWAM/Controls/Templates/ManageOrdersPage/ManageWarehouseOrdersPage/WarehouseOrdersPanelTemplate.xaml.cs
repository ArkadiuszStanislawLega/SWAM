using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.Manage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses;
using SWAM.Enumerators;
using SWAM.Models.Warehouse;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseOrdersPanelTemplate.xaml
    /// </summary>
    public partial class WarehouseOrdersPanelTemplate : UserControl
    {
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }
        public WarehouseOrdersPanelTemplate()
        {
            InitializeComponent();
        }

        private void SwitchBetweenOrdersAndCreating(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }

        private void AddNewWarehouseOrder_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.NewWarehouseOrder;
            SwitchBetweenOrdersAndCreating(new CreateNewWarehouseOrderTemplate());
        }

        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type warehouse order id in text box.
        /// Finding all warehouse ordier whose id contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(WarehouseOrderListViewModel.Instance.WarehouseOrders);
            filter.Filter = warehouseOrder =>
            {
                WarehouseOrder allWarehouseOrdersWhose = warehouseOrder as WarehouseOrder;
                return allWarehouseOrdersWhose.Id.ToString().Contains(this.FindWarehouseOrder.Text);
            };
        }
        #endregion

        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.WarehouseOrderProfile;
            if (sender is Button button && button.DataContext is WarehouseOrder customerOrder)
            {
                SwitchBetweenOrdersAndCreating(new WarehouseOrderProfileTemplate() { DataContext = customerOrder });
            }
        }
    }
}
