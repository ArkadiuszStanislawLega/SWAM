using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Templates.AdministratorPage;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : UserControl
    {
        #region Properties
        /// <summary>
        /// Instances of all bookmarks in AdministratorPage.
        /// </summary>
        Dictionary<BookmarkInPage, UserControl> _userControls = new Dictionary<BookmarkInPage, UserControl>(){
                { BookmarkInPage.WarehousesControlPanel, new WarehousesControlPanelTemplate()},
                { BookmarkInPage.UsersControlPanel, new UsersControlPanelTemplate() }
        };
        /// <summary>
        /// Current visible bookmark.
        /// </summary>
        UserControl _currentContent;
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

            this._userControls.TryGetValue(bookmarkAdministratorPage, out UserControl userControl);
            ChangeContext(userControl);
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
