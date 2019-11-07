using SWAM.Controls.Templates.CustomerPage;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using SWAM.Strings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ManageCustomersPage.xaml
    /// </summary>
    public partial class ManageCustomersPage : BasicPage
    {
        private Customer _currentlyLoadedCustomerProfile;
        #region Basic constructor
        public ManageCustomersPage()
        {
            InitializeComponent();

            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewCustomer;
        }
        #endregion
        #region ChangeContent
        /// <summary>
        /// Changing content for the new one in right section of this user control.
        /// </summary>
        /// <param name="newContent">Profile of user template or New user template.</param>
        private void ChangeContent(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }
        #endregion

        #region CreateCustomerProfile
        /// <summary>
        /// Made view of the user profile in right section.
        /// </summary>
        /// <param name="customer">Index number of UsersListItemTemplate in the users list.</param>
        /// <return>Chosen user profile.</return>
        private CustomerProfileTemplate CreateCustomerProfile(Customer customer)
            => new CustomerProfileTemplate() { DataContext = customer };
        #endregion
        #region AddNewUser_Click
        /// <summary>
        /// Action after click add new customer button.
        /// Change main content to customer profile.
        /// </summary>
        /// <param name="sender">Add new customer button.</param>
        /// <param name="e">Event clicked.</param>
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewCustomer;
            ChangeContent(new CreateNewCustomerTemplate());
        }
        #endregion
        #region RefreshData
        /// <summary>
        /// Refreshing data depends on <see cref="CurrentBookmarkLoaded"/>.
        /// </summary>
        public override void RefreshData()
        {
            if (this.CurrentBookmarkLoaded == BookmarkInPage.CustomerProfile)
            {
                CustomersListViewModel.Instance.Refresh();
                CustomerOrdersListViewModel.Instance.Refresh(this._currentlyLoadedCustomerProfile);
            }
            else if (this.CurrentBookmarkLoaded == BookmarkInPage.CreateNewCustomer) CustomersListViewModel.Instance.Refresh(); ;
        }
        #endregion
        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type customer name in text box.
        /// Finding all customers whose name contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(CustomersListViewModel.Instance.CustomersList);
            filter.Filter = customer =>
            {
                Customer allCustomersWhose = customer as Customer;
                return allCustomersWhose.Name.Contains(this.FindCustomer.Text);
            };
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after click sort ascending checkbox.
        /// </summary>
        /// <param name="sender">Sort ascending checkbox.</param>
        /// <param name="e">Event clicked.</param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (this.CustomersListView.Items.SortDescriptions.Count > 0)
                this.CustomersListView.Items.SortDescriptions.RemoveAt(this.CustomersListView.Items.SortDescriptions.Count - 1);

            if (SortAscending.IsChecked == true)
                this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            else
                this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion
        #region Item_Click
        /// <summary>
        /// Action after click item from customers list.
        /// Change main content to customer profile.
        /// </summary>
        /// <param name="sender">Item from customers list.</param>
        /// <param name="e">Event clicked.</param>
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Customer customer)
                {
                    this._currentlyLoadedCustomerProfile = customer;
                    this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CustomerProfile;
                    CustomerOrdersListViewModel.Instance.Refresh(this._currentlyLoadedCustomerProfile);
                    ChangeContent(CreateCustomerProfile(this._currentlyLoadedCustomerProfile));
                } 
            }
            else InformationToUser(ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR);
        }
        #endregion
    }
}
