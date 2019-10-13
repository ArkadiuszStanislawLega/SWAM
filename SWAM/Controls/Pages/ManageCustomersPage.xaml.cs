using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.CustomerPage;
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
    public partial class ManageCustomersPage : BasicUserControl
    {
        public ManageCustomersPage()
        {
            InitializeComponent();
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

        #region CreateUserProfile
        /// <summary>
        /// Made view of the user profile in right section.
        /// </summary>
        /// <param name="customer">Index number of UsersListItemTemplate in the users list.</param>
        /// <return>Chosen user profile.</return>
        private CustomerProfileTemplate CreateCustomerProfile(Customer customer) 
            => new CustomerProfileTemplate() { DataContext = customer };
        #endregion

        private void AddNewUser_Click(object sender, RoutedEventArgs e) => ChangeContent(new CreateNewCustomerTemplate());
        
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
            if (UsersList.Items.SortDescriptions.Count > 0)
                UsersList.Items.SortDescriptions.RemoveAt(UsersList.Items.SortDescriptions.Count - 1);

            if (SortAscending.IsChecked == true)
                UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            else
                UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Descending));
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
                ChangeContent(CreateCustomerProfile((Customer)button.DataContext));
            else InformationToUser(ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR);
        }
    }
}
