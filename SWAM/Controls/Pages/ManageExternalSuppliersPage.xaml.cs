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
        #region Properties
        /// <summary>
        /// If in page is loaded some profile here is the dataContext.
        /// </summary>
        public ExternalSupplier CurrentlyLoadedExternalSupplier { get; private set; } = new ExternalSupplier();
        #endregion
        #region Basic constructor
        public ManageExternalSuppliersPage()
        {
            InitializeComponent();

            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewExternalSupplier;
        }
        #endregion
    
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

        #region ChangeSorting
        /// <summary>
        /// Change type of sorting depends on user settings.
        /// </summary>
        private void ChangeSorting()
        {
            if (this.CustomersListView != null)
            {
                //Delete the last setting
                if (this.CustomersListView.Items.SortDescriptions.Count > 0)
                    this.CustomersListView.Items.SortDescriptions.RemoveAt(this.CustomersListView.Items.SortDescriptions.Count - 1);

                if (this.FiltrByName.IsChecked == true)
                {
                    if (this.SortAscending.IsChecked == true)
                        this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this.CurrentlyLoadedExternalSupplier.Name), System.ComponentModel.ListSortDirection.Ascending));
                    else
                        this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this.CurrentlyLoadedExternalSupplier.Name), System.ComponentModel.ListSortDirection.Descending));
                }
                else if (this.FiltrByTIN.IsChecked == true)
                {
                    if (this.SortAscending.IsChecked == true)
                        this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this.CurrentlyLoadedExternalSupplier.Tin), System.ComponentModel.ListSortDirection.Ascending));
                    else
                        this.CustomersListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this.CurrentlyLoadedExternalSupplier.Tin), System.ComponentModel.ListSortDirection.Descending));
                }
            }
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

        #region AddNewExternalSupplier_Click
        /// <summary>
        /// Action after click add new external supplier button.
        /// Changing content to create new external supplier template.
        /// </summary>
        /// <param name="sender">Add new external supplier button.</param>
        /// <param name="e">Event clicked.</param>
        private void AddNewExternalSupplier_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = Enumerators.BookmarkInPage.CreateNewExternalSupplier;
            ChangeContent(new CreateNewExternalSupplierTemplate());
        }
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
                return this.FiltrByName.IsChecked == true ? allExternalSupplierWhose.Name.Contains(this.FindExternalSupplier.Text) : allExternalSupplierWhose.Tin.Contains(this.FindExternalSupplier.Text);
            };
        }
        #endregion
        #region Item_Click
        /// <summary>
        /// Action after clicked item from externa suppliers list.
        /// Change MainContent of this template to external supplier profile template.
        /// </summary>
        /// <param name="sender">Item from the list.</param>
        /// <param name="e">Event clicked.</param>
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
        #endregion
        #region FiltrByName_Checked
        /// <summary>
        /// Action after clicked filter by name radio button.
        /// Changing type of sorting list with external suppliers.
        /// </summary>
        /// <param name="sender">Filter by name radio button.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByName_Checked(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
        #region FiltrByTIN_Checked
        /// <summary>
        /// Action after clicked filter TIN radio button.
        /// Changing type of sorting list with external suppliers.
        /// </summary>
        /// <param name="sender">Filter by TIN radio button.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByTIN_Checked(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after clicked filter sorting ascending checkbox.
        /// Changing type of sorting list with external suppliers.
        /// </summary>
        /// <param name="sender">Sort ascending checkbox.</param>
        /// <param name="e">Event is clicked.</param>
        private void SortAscending_Click(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
    }
}
