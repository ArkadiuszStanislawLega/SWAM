using SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.Manage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder.Customers;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrdersPanelTemplate.xaml
    /// </summary>
    public partial class CustomerOrdersPanelTemplate : UserControl
    {
        #region Properties
        /// <summary>
        /// It is required for customer order properties names.
        /// </summary>
        public CustomerOrder _defaultCustomerOrder = new CustomerOrder();
        /// <summary>
        /// Current bookmark loaded in main content.
        /// </summary>
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }
        /// <summary>
        /// Current filter on customer orders list.
        /// </summary>
        private readonly ICollectionView _filter = CollectionViewSource.GetDefaultView(Models.ManageOrdersPage.CustomerOrdersListViewModel.Instance.CustomerOrders);
        #endregion
        #region Basic Constructor
        public CustomerOrdersPanelTemplate()
        {
            InitializeComponent();

            this.OrderStatus.SelectedItem = CustomerOrderStatus.Delivered;
        }
        #endregion

        #region SwitchBetweenOrdersAndCreating
        /// <summary>
        /// Change main content between new customer order template or order profile.
        /// </summary>
        /// <param name="newContent">Content to be inserted.</param>
        private void SwitchBetweenOrdersAndCreating(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }
        #endregion

        #region ChangeSortingType
        /// <summary>
        /// Sets the list rotation depending on the settings made.
        /// </summary>
        private void ChangeSortingType()
        {
            if (this.CustomersListView != null)
            {
                //Delete the last setting
                if (this.CustomersListView.Items.SortDescriptions.Count > 0)
                    this.CustomersListView.Items.SortDescriptions.RemoveAt(this.CustomersListView.Items.SortDescriptions.Count - 1);

                //Get the name of the property by which to sort the list.
                string sortByProperty = string.Empty;
                if (this.FiltrByCustomerOrderStatus.IsChecked == true) sortByProperty = nameof(this._defaultCustomerOrder.CustomerOrderStatus);
                else if (this.FiltrByDateOfCreate.IsChecked == true) sortByProperty = nameof(this._defaultCustomerOrder.OrderDate);
                else if (this.FiltrById.IsChecked == true) sortByProperty = nameof(this._defaultCustomerOrder.Id);

                //Depends on SortAscending is checked or not chose type od sorting.
                if (this.SortAscending.IsChecked == true)
                    this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(sortByProperty, System.ComponentModel.ListSortDirection.Ascending));
                else
                    this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(sortByProperty, System.ComponentModel.ListSortDirection.Descending));
            }
        }
        #endregion

        #region FiltrListById
        /// <summary>
        /// Filters the order list by ID number.
        /// </summary>
        private void FiltrListById()
        {
            if (this.FiltrById.IsChecked == true)
            {
                this._filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;
                    return allCustomerOrdersWhose.Id.ToString().Contains(this.FindCustomerOrder.Text);
                };
                ChangeSortingType();
            }
        }
        #endregion
        #region FiltrListByDateOfCreate
        /// <summary>
        /// Filters the order list by date of create.
        /// </summary>
        private void FiltrListByDateOfCreate()
        {
            if (this.FiltrByDateOfCreate.IsChecked == true)
            {
                this._filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;
                    return allCustomerOrdersWhose.OrderDate.ToString().Contains(this.FindCustomerOrder.Text);
                };
                ChangeSortingType();
            }
        }
        #endregion
        #region FiltrListByCustomerOrderStatus
        /// <summary>
        /// Filters the order list by customer order status.
        /// </summary>
        private void FiltrListByCustomerOrderStatus()
        {
            if (this.FiltrByCustomerOrderStatus.IsChecked == true)
            {
                if (this.OrderStatus.SelectedValue != null)
                {
                    var selectedValue = (CustomerOrderStatus)this.OrderStatus.SelectedValue;

                    this._filter.Filter = customerOrder =>
                    {
                        CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;
                        return allCustomerOrdersWhose.CustomerOrderStatus.ToString().Contains(selectedValue.ToString());
                    };
                }
                ChangeSortingType();
            }
        }
        #endregion

        #region GenerateFilter
        /// <summary>
        /// Generate filter when the items from combobox are change.
        /// </summary>
        private void GenerateFilter()
        {
            FiltrListById();
            FiltrListByDateOfCreate();
            FiltrListByCustomerOrderStatus();
        }
        #endregion

        #region FiltrById_Checked
        /// <summary>
        /// Action after filtr by Id radio button is checked.
        /// Sets sorting the list according to Order Id.
        /// </summary>
        /// <param name="sender">Order by Id radio button.</param>
        /// <param name="e">Event set IsCheced on true.</param>
        private void FiltrById_Checked(object sender, RoutedEventArgs e) => FiltrListById();
        #endregion
        #region FiltrByDateOfCreate_Checked
        /// <summary>
        /// Action after filtr by date of create radio button is checked.
        /// Sets sorting the list by order creation date.
        /// </summary>
        /// <param name="sender">Order by creation date radio button.</param>
        /// <param name="e">Event set IsCheced on true.</param>
        private void FiltrByDateOfCreate_Checked(object sender, RoutedEventArgs e) => FiltrListByDateOfCreate();
        #endregion
        #region FiltrByCustomerOrderStatus_Checked
        /// <summary>
        /// Action after change checked radio button filter by customer order status.
        /// Change filtering list by customer order status.
        /// </summary>
        /// <param name="sender">Radio button filtr by customer order status.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByCustomerOrderStatus_Checked(object sender, RoutedEventArgs e) => FiltrListByCustomerOrderStatus();
        #endregion
       
        #region OrderStatus_SelectionChanged
        /// <summary>
        /// Action after change value in combo box order status.
        /// Change filtering list by current selected value.
        /// </summary>
        /// <param name="sender">Combo box order status.</param>
        /// <param name="e">Event change value.</param>
        private void OrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e) => GenerateFilter();
        #endregion

        #region AddNewCustomerOrder_Click
        /// <summary>
        /// Action after click add new customer order button.
        /// Change content to create new customer order template.
        /// </summary>
        /// <param name="sender">Button add new customer order.</param>
        /// <param name="e">Event clicked.</param>
        private void AddNewCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCustomerOrder;
            SwitchBetweenOrdersAndCreating(new CreateNewCustomerOrderTemplate());
        }
        #endregion
        #region Item_Click
        /// <summary>
        /// Action after click any of item in customer orders list.
        /// Showing profile of the order.
        /// </summary>
        /// <param name="sender">Item from the list.</param>
        /// <param name="e">Event clicked.</param>
        private void Item_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CustomerOrderProfile;
            if (sender is Button button && button.DataContext is CustomerOrder customerOrder)
            {
                SwitchBetweenOrdersAndCreating(new CustomerOrderProfileTemplate() { DataContext = customerOrder });
            }
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after sort ascending checkbox is checked.
        /// Sorts items by the selected option.
        /// </summary>
        /// <param name="sender">Sort ascending checkbox.</param>
        /// <param name="e">Event is clicked.</param>
        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e) => ChangeSortingType();
        #endregion

        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type customer order id in text box.
        /// Finding all customer ordier whose id contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.FiltrById.IsChecked == true)
            {
                this._filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;

                    return allCustomerOrdersWhose.Id.ToString().Contains(this.FindCustomerOrder.Text);
                };
            }
            else if (this.FiltrByDateOfCreate.IsChecked == true)
            {
                this._filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;

                    return allCustomerOrdersWhose.OrderDate.ToString().Contains(this.FindCustomerOrder.Text);
                };
            }
        }
        #endregion
    }
}
