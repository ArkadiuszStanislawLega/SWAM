using SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.Manage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder.Customers;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using SWAM.Models.ManageOrdersPage;
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
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }

        private ICollectionView _filter = CollectionViewSource.GetDefaultView(Models.ManageOrdersPage.CustomerOrdersListViewModel.Instance.CustomerOrders);

        public CustomerOrdersPanelTemplate()
        {
            InitializeComponent();
        }

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
        }
        #endregion

        private void GenerateFilter()
        {
            if (this.FiltrByCustomerOrderStatus.IsChecked == true)
            {
                var selectedValue = (CustomerOrderStatus)this.OrderStatus.SelectedValue;

                this._filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;
                    return allCustomerOrdersWhose.CustomerOrderStatus.ToString().Contains(selectedValue.ToString());
                };
            }
        }
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

        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

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
        #region FiltrByCustomerOrderStatus_Checked
        /// <summary>
        /// Action after change checked radio button filter by customer order status.
        /// Change filtering list by customer order status.
        /// </summary>
        /// <param name="sender">Radio button filtr by customer order status.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByCustomerOrderStatus_Checked(object sender, RoutedEventArgs e) => GenerateFilter();
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
    }
}
