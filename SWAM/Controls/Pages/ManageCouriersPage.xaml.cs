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
        private Courier _currentlyLoadedCourierProfile;
        #region Basic constructor
        public ManageCouriersPage()
        {
            InitializeComponent();

            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCourier;
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
                return  allCouriersWhose.Name.Contains(this.FindCourier.Text);
            };
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Sorting list of courier ascending or descanding.
        /// </summary>
        /// <param name="sender">Checkbox</param>
        /// <param name="e">Clicked.</param>
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
        #endregion
    }
}
