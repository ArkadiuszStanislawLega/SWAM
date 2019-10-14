using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.CouriersPage;
using SWAM.Models.Customer;
using SWAM.Strings;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ManageCouriersPage.xaml
    /// </summary>
    public partial class ManageCouriersPage : BasicUserControl
    {
        public ManageCouriersPage()
        {
            InitializeComponent();
            ChangeContent(new CreateNewCourierTemplate());
        }

        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {

        }

        #region CreateCourierProfile
        /// <summary>
        /// Made view of the courier profile in right section.
        /// </summary>
        /// <param name="customer">Index number of CouriersListItemTemplate in the couriers list.</param>
        /// <return>Chosen courier profile.</return>
        private CourierProfileTemplate CreateCourierProfile(Customer customer)
            => new CourierProfileTemplate() { DataContext = customer };
        #endregion

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                ChangeContent(CreateCourierProfile((Customer)button.DataContext));
            }
            else InformationToUser(ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR);
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO: Create this couriers list view model
            //filter is required observable collection.
            //ICollectionView filter = CollectionViewSource.GetDefaultView(CustomersListViewModel.Instance);
            //filter.Filter = customer =>
            //{
            //    Customer allCustomersWhose = customer as Customer;
            //    return this.FiltrByName.IsChecked == true ? allCustomersWhose.Name.Contains(this.FindCustomer.Text) : allCustomersWhose.Surname.Contains(this.FindCustomer.Text);
            //};
        }

        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (CouriersList.Items.SortDescriptions.Count > 0)
                CouriersList.Items.SortDescriptions.RemoveAt(CouriersList.Items.SortDescriptions.Count - 1);

            if (SortAscending.IsChecked == true)
                CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            else
                CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Descending));
        }

    }
}
