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
        #region Properties
        private WarehouseOrder _defaultWarehuseOrder = new WarehouseOrder();
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }
        /// <summary>
        /// Current filter on customer orders list.
        /// </summary>
        private readonly ICollectionView _filter = CollectionViewSource.GetDefaultView(WarehouseOrderListViewModel.Instance.WarehouseOrders);
        #endregion
        #region Basic constructor
        public WarehouseOrdersPanelTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region SwitchBetweenOrdersAndCreating
        /// <summary>
        /// Changing main content of this view.
        /// </summary>
        /// <param name="newContent">New content of main view.</param>
        private void SwitchBetweenOrdersAndCreating(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }
        #endregion
 
        #region FilterListById
        /// <summary>
        /// Filters the order list by order Id.
        /// </summary>
        private void FilterListById()
        {
            //filter is required observable collection.
            this._filter.Filter = warehouseOrder =>
            {
                WarehouseOrder allWarehouseOrdersWhose = warehouseOrder as WarehouseOrder;
                return allWarehouseOrdersWhose.Id.ToString().Contains(this.FindWarehouseOrder.Text);
            };
        }
        #endregion
        #region FilterListByWarehouseOrderStatus
        /// <summary>
        /// Filters the order list by warehouse order status.
        /// </summary>
        private void FilterListByWarehouseOrderStatus()
        {
            if (this.FiltrByWarehouseOrderStatus.IsChecked == true)
            {
                if (this.OrderStatus.SelectedValue != null)
                {
                    var selectedValue = (WarehouseOrderStatus)this.OrderStatus.SelectedValue;

                    this._filter.Filter = warehouseOrder =>
                    {
                        WarehouseOrder allWarehouseOrdersWhose = warehouseOrder as WarehouseOrder;
                        return allWarehouseOrdersWhose.WarehouseOrderStatus.ToString().Contains(selectedValue.ToString());
                    };
                }
            }
        }
        #endregion

        #region FiltrById_Checked
        /// <summary>
        /// Action after click filter by Id radio button.
        /// </summary>
        /// <param name="sender">Radio button filtr by Id.</param>
        /// <param name="e">Event checked.</param>
        private void FiltrById_Checked(object sender, RoutedEventArgs e) => FilterListById();
        #endregion
        #region FiltrByWarehouseOrderStatus_Checked
        /// <summary>
        /// Action after checked filter by warehouse order status radio button.
        /// </summary>
        /// <param name="sender">Radio button filter by warehouse order status.</param>
        /// <param name="e">Event radio button checked.</param>
        private void FiltrByWarehouseOrderStatus_Checked(object sender, RoutedEventArgs e) => FilterListByWarehouseOrderStatus();
        #endregion

        #region Item_Click
        /// <summary>
        /// Show the profile of selected order.
        /// </summary>
        /// <param name="sender">Item from the list with orders.</param>
        /// <param name="e">Event select item.</param>
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.WarehouseOrderProfile;
            if (sender is Button button && button.DataContext is WarehouseOrder customerOrder)
            {
                SwitchBetweenOrdersAndCreating(new WarehouseOrderProfileTemplate() { DataContext = customerOrder });
            }
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Sorts the list depending on the checked radiobutton descending or ascending.
        /// </summary>
        /// <param name="sender">Radio button.</param>
        /// <param name="e">Event check or unchecked.</param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            if (this.WarehouseOrderListView.Items.SortDescriptions.Count > 0)
                this.WarehouseOrderListView.Items.SortDescriptions.RemoveAt(this.WarehouseOrderListView.Items.SortDescriptions.Count - 1);

            string sortByProperty = string.Empty;
            if (this.FiltrByWarehouseOrderStatus.IsChecked == true) sortByProperty = nameof(this._defaultWarehuseOrder.WarehouseOrderStatus);
            else if (this.FiltrById.IsChecked == true) sortByProperty = nameof(this._defaultWarehuseOrder.Id);

            if (this.SortAscending.IsChecked == true)
                this.WarehouseOrderListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(sortByProperty, System.ComponentModel.ListSortDirection.Ascending));
            else
                this.WarehouseOrderListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(sortByProperty, System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion
        #region AddNewWarehouseOrder_Click
        /// <summary>
        /// Action after clicked button add new warehouse order.
        /// </summary>
        /// <param name="sender">Button add new warehouse order.</param>
        /// <param name="e">Event button is clicked</param>
        private void AddNewWarehouseOrder_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.NewWarehouseOrder;
            SwitchBetweenOrdersAndCreating(new CreateNewWarehouseOrderTemplate());
        }
        #endregion

        #region OrderStatus_SelectionChanged
        /// <summary>
        /// Action after change type of status warehouse order.
        /// </summary>
        /// <param name="sender">Item from combo box.</param>
        /// <param name="e">Event clicked.</param>
        private void OrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e) => FilterListByWarehouseOrderStatus();
        #endregion

        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type warehouse order id in text box.
        /// Finding all warehouse ordier whose id contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => FilterListById();
        #endregion

    }
}
