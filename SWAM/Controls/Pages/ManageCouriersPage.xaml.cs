using SWAM.Controls.Templates.CouriersPage;
using SWAM.Enumerators;
using SWAM.Models.Courier;
using SWAM.Strings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ManageCouriersPage.xaml
    /// </summary>
    public partial class ManageCouriersPage : BasicPage
    {
        private Courier _currentlyLoadedCourierProfile = new Courier();
        #region Basic constructor
        public ManageCouriersPage()
        {
            InitializeComponent();

            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCourier;
            ChangeSorting();
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh lists depends on <see cref="CurrentBookmarkLoaded"/>.
        /// </summary>
        public override void RefreshData()
        {
            if (this.CurrentBookmarkLoaded == BookmarkInPage.CourierProfile)
            {
                CouriersListViewModel.Instance.Refresh();
                CourierOrdersListViewModel.Instance.Refresh(this._currentlyLoadedCourierProfile);
            }
            else if (this.CurrentBookmarkLoaded == BookmarkInPage.CreateNewCourier) CouriersListViewModel.Instance.Refresh();
        }
        #endregion
        #region CreateCourierProfile
        /// <summary>
        /// Made view of the courier profile in right section.
        /// </summary>
        /// <param name="courier">Index number of CouriersListItemTemplate in the couriers list.</param>
        /// <return>Chosen courier profile.</return>
        private CourierProfileTemplate CreateCourierProfile(Courier courier)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CourierProfile;
            return new CourierProfileTemplate() { DataContext = courier };
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

            if (newContent != null)
                this.MainContent.Children.Add(newContent);
        }
        #endregion
        #region ChangeSorting
        /// <summary>
        /// Change type of sorting depends on user settings.
        /// </summary>
        private void ChangeSorting()
        {
            if (this.CouriersList != null)
            {
                //Delete the last setting
                if (this.CouriersList.Items.SortDescriptions.Count > 0)
                    this.CouriersList.Items.SortDescriptions.RemoveAt(this.CouriersList.Items.SortDescriptions.Count - 1);

                if (this.FiltrByName.IsChecked == true)
                {
                    if (this.SortAscending.IsChecked == true)
                        this.CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this._currentlyLoadedCourierProfile.Name), System.ComponentModel.ListSortDirection.Ascending));
                    else
                        this.CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this._currentlyLoadedCourierProfile.Name), System.ComponentModel.ListSortDirection.Descending));
                }
                else if (this.FiltrByTIN.IsChecked == true)
                {
                    if (this.SortAscending.IsChecked == true)
                        this.CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this._currentlyLoadedCourierProfile.Tin), System.ComponentModel.ListSortDirection.Ascending));
                    else
                        this.CouriersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(this._currentlyLoadedCourierProfile.Tin), System.ComponentModel.ListSortDirection.Descending));
                }
            }
        }
        #endregion

        #region AddNewCourier_Click
        /// <summary>
        /// Action after add new courier button is clicked.
        /// Changing content to create new courier template.
        /// </summary>
        /// <param name="sender">Button add new courier.</param>
        /// <param name="e">Event clicked.</param>
        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentBookmarkLoaded != BookmarkInPage.CreateNewCourier)
            {
                this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCourier;
                ChangeContent(new CreateNewCourierTemplate());
            }
        }
        #endregion
        #region Item_Click
        /// <summary>
        /// Action after click item from the couriers list.
        /// Adding courier profile to MainContent of this template.
        /// </summary>
        /// <param name="sender">Item from the couriers list.</param>
        /// <param name="e">Clicked.</param>
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if ((Courier)button.DataContext is Courier courier)
                {
                    this._currentlyLoadedCourierProfile = courier;

                    CourierOrdersListViewModel.Instance.Refresh(courier);
                    ChangeContent(CreateCourierProfile((Courier)button.DataContext));
                }
                else InformationToUser($"{ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}");
            }
            else InformationToUser(ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR);
        }
        #endregion
        #region TextBox_TextChanged
        /// <summary>
        /// Action when user type courier name in text box.
        /// Finding all couriers whose name contains typed letters.
        /// </summary>
        /// <param name="sender">Text box.</param>
        /// <param name="e">Event typed letter.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(CouriersListViewModel.Instance.CouriersList);
            filter.Filter = couriers =>
            {
                Courier allCouriersWhose = couriers as Courier;
                return this.FiltrByName.IsChecked == true ? allCouriersWhose.Name.Contains(this.FindCourier.Text) : allCouriersWhose.Tin.Contains(this.FindCourier.Text);
            };
        }
        #endregion

        #region FiltrByName_Checked
        /// <summary>
        /// Action after clicked filter by name radio button.
        /// Changing type of sorting list with couriers.
        /// </summary>
        /// <param name="sender">Filter by name radio button.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByName_Checked(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
        #region FiltrByTIN_Checked
        /// <summary>
        /// Action after clicked filter TIN radio button.
        /// Changing type of sorting list with couriers.
        /// </summary>
        /// <param name="sender">Filter by TIN radio button.</param>
        /// <param name="e">Event is checked.</param>
        private void FiltrByTIN_Checked(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after clicked filter sorting ascending checkbox.
        /// Changing type of sorting list with couriers.
        /// </summary>
        /// <param name="sender">Sort ascending checkbox.</param>
        /// <param name="e">Event is clicked.</param>
        private void SortAscending_Click(object sender, RoutedEventArgs e) => ChangeSorting();
        #endregion
    }
}
