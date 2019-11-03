using SWAM.Controls.Templates.ManageOrdersPage.Manage;
using SWAM.Controls.Templates.ManageOrdersPage.NewOrder.Customers;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using SWAM.Models.ManageOrdersPage;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrdersPanelTemplate.xaml
    /// </summary>
    public partial class CustomerOrdersPanelTemplate : UserControl
    {
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }

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
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(Models.ManageOrdersPage.CustomerOrdersListViewModel.Instance.CustomerOrders);
            filter.Filter = customerOrder =>
            {
                CustomerOrder allCustomerOrdersWhose = customerOrder as CustomerOrder;
                return allCustomerOrdersWhose.Id.ToString().Contains(this.FindCustomerOrder.Text);
            };
        }
        #endregion
        private void SwitchBetweenOrdersAndCreating(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }

        private void AddNewCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCustomerOrder;
            SwitchBetweenOrdersAndCreating(new CreateNewCustomerOrderTemplate());
        }


        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Item_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CustomerOrderProfile;
            if (sender is Button button && button.DataContext is CustomerOrder customerOrder)
            {
                SwitchBetweenOrdersAndCreating(new CustomerOrderProfileTemplate() { DataContext = customerOrder });
            }
        }

    }
}
