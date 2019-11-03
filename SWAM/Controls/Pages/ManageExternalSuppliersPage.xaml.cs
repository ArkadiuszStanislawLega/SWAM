using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.ExternalSupplierPage;
using SWAM.Enumerators;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ManageExternalSuppliersPage.xaml
    /// </summary>
    public partial class ManageExternalSuppliersPage : BasicPage
    {
        /// <summary>
        /// If in page is loaded some profile here is the dataContext.
        /// </summary>
        public ExternalSupplier CurrentlyLoadedExternalSupplier { get; private set; }
        public ManageExternalSuppliersPage()
        {
            InitializeComponent();

            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewExternalSupplier;
        }

        private void AddNewExternalSupplier_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewExternalSupplier;
            ChangeContent(new CreateNewExternalSupplierTemplate());
        }

        #region RefreshingData
        /// <summary>
        /// Refreshing data depends on <see cref="CurrentBookmarkLoaded"/>.
        /// </summary>
        public override void RefreshData()
        {
            if (this.CurrentBookmarkLoaded == BookmarkInPage.ExternalSupplierProfile)
            {
                ExternalSupplierListViewModel.Instance.Refresh();
                ExternalSupplierDeliveryListViewModel.Instance.Refresh(this.CurrentlyLoadedExternalSupplier);
            }
            else if (this.CurrentBookmarkLoaded == BookmarkInPage.CreateNewExternalSupplier) ExternalSupplierListViewModel.Instance.Refresh(); ;
        }
        #endregion

        #region CreateCustomerProfile
        /// <summary>
        /// Made view of the external supplier profile in right section.
        /// </summary>
        /// <param name="externalSupplier">Index number of ExternalSupplierListViewModel in the users list.</param>
        /// <return>Chosen user profile.</return>
        private ExternalSupplierProfileTemplate CreateExternalSupplierProfile() => new ExternalSupplierProfileTemplate() { DataContext = this.CurrentlyLoadedExternalSupplier };
        #endregion
        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type external supplier name in text box.
        /// Finding all external suppliers whose name contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(ExternalSupplierListViewModel.Instance.ExternalSuppliers);
            filter.Filter = externalSupplier =>
            {
                ExternalSupplier allExternalSupplierWhose = externalSupplier as ExternalSupplier;
                return this.FiltrByName.IsChecked == true ? allExternalSupplierWhose.Name.Contains(this.FindExternalSupplier.Text) : allExternalSupplierWhose.Name.Contains(this.FindExternalSupplier.Text);
            };
        }
        #endregion
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

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is ExternalSupplier external)
                {
                    this.CurrentlyLoadedExternalSupplier = external;
                    this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.ExternalSupplierProfile;
                    ExternalSupplierDeliveryListViewModel.Instance.Refresh(this.CurrentlyLoadedExternalSupplier);
                    ChangeContent(CreateExternalSupplierProfile());
                }
            }
            else InformationToUser(ErrorMesages.REFRESH_EXTERNAL_SUPPLIER_PROFILE_ERROR);
        }
    }
}
