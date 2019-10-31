using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Models.AdministratorPage;
using SWAM.Templates.AdministratorPage;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : BasicPage
    {
        #region Properties
        /// <summary>
        /// Instances of all bookmarks in AdministratorPage.
        /// </summary>
        private Dictionary<BookmarkInPage, UserControl> _userControls = new Dictionary<BookmarkInPage, UserControl>(){
                { BookmarkInPage.WarehousesControlPanel, new WarehousesControlPanelTemplate()},
                { BookmarkInPage.UsersControlPanel, new UsersControlPanelTemplate() }
        };
        /// <summary>
        /// Current visible bookmark.
        /// </summary>
        private UserControl _currentContent;
        #endregion

        #region Basic Constructor
        public AdministratorPage()
        {
            InitializeComponent();
        }
        #endregion

        #region OnInitialized
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ChangeThisContent(BookmarkInPage.UsersControlPanel);
        }
        #endregion
        #region RefreshData
        /// <summary>
        /// Refreshing data depends on <see cref="CurrentBookmarkLoaded"/>.
        /// </summary>
        public void RefreshData()
        {
            if (this.CurrentBookmarkLoaded == BookmarkInPage.UsersControlPanel) UsersListViewModel.Instance.Refresh();
            else if (this.CurrentBookmarkLoaded == BookmarkInPage.WarehousesControlPanel) WarehousesListViewModel.Instance.Refresh();
        }
        #endregion
        private void WarhousesControlPanelBarPage_Click(object sender, RoutedEventArgs e) => ChangeThisContent(BookmarkInPage.WarehousesControlPanel);
        private void UsersControlPanelBar_Click(object sender, RoutedEventArgs e) => ChangeThisContent(BookmarkInPage.UsersControlPanel);

        #region ChangeThisContent
        /// <summary>
        /// Setting properties buttons depending on whether it is pressed.
        /// And changing main content of page.
        /// </summary>
        /// <param name="bookmarkAdministratorPage">New bookmark.</param>
        private void ChangeThisContent(BookmarkInPage bookmarkAdministratorPage)
        {
            //Find selected button
            foreach (NavigationButtonTemplate nvb in this.NavigationBar.Children)
            {
                if (nvb.Bookmark == bookmarkAdministratorPage) nvb.IsSelected = true;
                else nvb.IsSelected = false;
            }

            if (this._userControls.TryGetValue(bookmarkAdministratorPage, out UserControl userControl))
            {
                ChangeContext(userControl);
                this.CurrentBookmarkLoaded = bookmarkAdministratorPage;
            }
        }
        #endregion
        #region ChangeContext
        /// <summary>
        /// Changing main content of AdministratorPage.
        /// </summary>
        /// <param name="administratorPageControlPanels">New content.</param>
        private void ChangeContext(UserControl administratorPageControlPanels)
        {
            if (administratorPageControlPanels != this._currentContent)
            {
                this._currentContent = administratorPageControlPanels;
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Capacity - 1);
                this.MainContent.Children.Add(this._currentContent);
            }
        }
        #endregion

    }
}
