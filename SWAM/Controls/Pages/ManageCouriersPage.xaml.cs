using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.CouriersPage;
using SWAM.Enumerators;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
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
        public ManageCouriersPage()
        {
            InitializeComponent();

            ChangeContent(new CreateNewCourierTemplate());
            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCourier;
        }

        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CourierProfile;
            ChangeContent(new CreateNewCourierTemplate());
        }

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
        /// <param name="customer">Index number of CouriersListItemTemplate in the couriers list.</param>
        /// <return>Chosen courier profile.</return>
        private CourierProfileTemplate CreateCourierProfile(Courier customer)
            => new CourierProfileTemplate() { DataContext = customer };
        #endregion

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if ((Courier)button.DataContext is Courier courier)
                {
                    this._currentlyLoadedCourierProfile = courier;
                    this.CurrentBookmarkLoaded = BookmarkInPage.CourierProfile;

                    CourierOrdersListViewModel.Instance.Refresh(courier);
                    ChangeContent(CreateCourierProfile((Courier)button.DataContext));
                }
                else InformationToUser($"{ErrorMesages.REFRESH_CUSTOMER_PROFILE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}");
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
