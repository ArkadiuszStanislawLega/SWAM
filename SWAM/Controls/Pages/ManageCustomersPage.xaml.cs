using SWAM.Controls.Templates.AdministratorPage;
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
        public ManageCustomersPage()
        {
            InitializeComponent();
            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewCustomer;
            ChangeContent(new CreateNewCustomerTemplate());
        }
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

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewCustomer;
            ChangeContent(new CreateNewCustomerTemplate());
        }
        #region RefreshData
        /// <summary>
        /// Refreshing data depends on <see cref="CurrentBookmarkLoaded"/>.
        /// </summary>
        public void RefreshData()
        {
            if (this.CurrentBookmarkLoaded == BookmarkInPage.CustomerProfile)
            {
                CustomersListViewModel.Instance.Refresh();
                CustomerOrdersListViewModel.Instance.Refresh(this._currentlyLoadedCustomerProfile);
            }
            else if (this.CurrentBookmarkLoaded == BookmarkInPage.CreateNewCustomer) CustomersListViewModel.Instance.Refresh(); ;
        }
        #endregion  
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(CustomersListViewModel.Instance);
            filter.Filter = customer =>
            {
                Customer allCustomersWhose = customer as Customer;
                return this.FiltrByName.IsChecked == true ? allCustomersWhose.Name.Contains(this.FindCustomer.Text) : allCustomersWhose.Surname.Contains(this.FindCustomer.Text);
            };
        }

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
    }
}
