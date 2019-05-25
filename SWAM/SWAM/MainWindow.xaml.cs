using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SWAM.Controls.Pages;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Models;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Current user logged in to application.
        /// </summary>
        public static User LoggedInUser = new User
        {
            Id = 1,
            Name = "Admin",
            Password = "haslowo",
            Permissions = UserType.Programmer
        };
        /// <summary>
        /// Flag indication that user is logged in or not.
        /// </summary>
        public static bool IsLoggedIn = true;

        #region Properties
        /// <summary>
        /// Flag indicating whether the application is maximized.
        /// </summary>
        private bool _IsMaximized = false;
        /// <summary>
        /// Indicates which page is currently loaded.
        /// </summary>
        private PagesUserControls _currentPageLoaded;
        /// <summary>
        /// Container of the main content of application.
        /// </summary>
        private StackPanel _pageContainer;
        /// <summary>
        /// Container with whole pages view.
        /// </summary>
        private Dictionary<PagesUserControls, UserControl> _pages = new Dictionary<PagesUserControls, UserControl>()
        {
            { PagesUserControls.LoginPage,new LoginPage() },
            { PagesUserControls.AdministratorPage, new AdministratorPage()},
            { PagesUserControls.ManageItemsPage, new ManageItemPage()},
            { PagesUserControls.ManageMagazinePage, new ManageMagazinePage() },
            { PagesUserControls.ManageOrdersPage, new ManageOrdersPage() }
        };
        /// <summary>
        /// Container with whole priviligase of UserType.
        /// </summary>
        private Dictionary<UserType, List<PagesUserControls>> _pagesForUser = new Dictionary<UserType, List<PagesUserControls>>()
        {
           //Settings for administrator
           { UserType.Administrator,
                new List<PagesUserControls>(){ PagesUserControls.LoginPage,
                                          /**/ PagesUserControls.AdministratorPage }},
           //Settings for Seller
           { UserType.Seller,
                new List<PagesUserControls>(){ PagesUserControls.LoginPage,
                                          /**/ PagesUserControls.ManageMagazinePage }},
           //Setting for Waegouseman
           { UserType.Warehouseman,
                new List<PagesUserControls>(){ PagesUserControls.LoginPage,
                                          /**/ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage }},
            { UserType.Programmer,
                new List<PagesUserControls>(){ PagesUserControls.LoginPage,
                                          /**/ PagesUserControls.AdministratorPage,
                                          /**/ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage }},
        };
        #endregion

        #region Setters
        /// <summary>
        ///  Indicates which page is currently loaded.
        /// </summary>
        public PagesUserControls CurrentPageLoaded { get => this._currentPageLoaded; set => this._currentPageLoaded = value; }
        #endregion

        #region BasicConstructor
        public MainWindow()
        {
            InitializeComponent();

            this._pageContainer = ContentOfWindow;

            ChangeContent(PagesUserControls.LoginPage);
            SetNavigationsButtonPagesContent();
            ChangeApplicationDependsOnUserPermissions();          
        }
        #endregion
        #region Window Functions Buttons
        #region TopBarContent_MouseDown
        /// <summary>
        /// Moving whole application window after drag top bar of application, when the left button was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopBarContent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion
        #region Maximize_Click
        /// <summary>
        /// Maximizing size of the application when the application size is smaller then max size. 
        /// Setting standard size of the application when the application is maximized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (!this._IsMaximized)
            {
                this.WindowState = WindowState.Maximized;
                this._IsMaximized = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this._IsMaximized = false;
            }
        }

        #endregion
        #region Minimize_Click
        /// <summary>
        /// Minimalizing size of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion
        #region Exit_Click
        /// <summary>
        /// Action after click in top bar right corner button.
        /// Closing application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #endregion

        #region Change main content of the application
        /// <summary>
        /// Change current view of application to chosen one.
        /// </summary>
        /// <param name="page">The Page which should be loaded.</param>
        public void ChangeContent(PagesUserControls page)
        {
            if (this._pageContainer.Children.Capacity > 0 && page != this._currentPageLoaded)
                this._pageContainer.Children.RemoveAt(_pageContainer.Children.Count - 1);

            if (page != this._currentPageLoaded)
            {
                UserControl pagetoAdd;
                this._pages.TryGetValue(page, out pagetoAdd);
                this._pageContainer.Children.Add(pagetoAdd);
            }
        }
        #endregion

        #region Navigation bar stuff 
        #region NaviagionBar_Click
        /// <summary>
        /// Navigation bar - buttonClicker.
        /// It is for fast navigation between pages.
        /// </summary>
        /// <param name="sender">NavigationButtonTemplate is required!</param>
        /// <param name="e"></param>
        private void NaviagionBar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as NavigationButtonTemplate;
            ChangeContent(button.PageToOpen);
            this._currentPageLoaded = button.PageToOpen;

            foreach (NavigationButtonTemplate nvb in this.NavigationBar.Children)
            {
                if (this._currentPageLoaded == nvb.PageToOpen) nvb.IsSelected = true;
                else nvb.IsSelected = false;
            }
        }
        #endregion
        #region SetNavigationsButtonPagesContent
        /// <summary>
        /// Setting property Pages in navigation buttons representing the value of the page to open.
        /// </summary>
        private void SetNavigationsButtonPagesContent()
        {
            this.SwitchToAdministratorPage.PageToOpen = PagesUserControls.AdministratorPage;
            this.SwitchToLoginPage.PageToOpen = PagesUserControls.LoginPage;
            this.SwitchToManageItemPage.PageToOpen = PagesUserControls.ManageItemsPage;
            this.SwitchToManageMagazinePage.PageToOpen = PagesUserControls.ManageMagazinePage;
            this.SwitchToManageOrderPage.PageToOpen = PagesUserControls.ManageOrdersPage;
        }
        #endregion
        #endregion

        #region ChangeApplicationDependsOnUserPermissions
        /// <summary>
        /// Its changes navigation buttons visible or not depends on loged in user permissions.
        /// </summary>
        private void ChangeApplicationDependsOnUserPermissions()
        {
            if (IsLoggedIn)
            {
                //Getting list of priviliges
                List<PagesUserControls> listWithPermissions;
                this._pagesForUser.TryGetValue(LoggedInUser.Permissions, out listWithPermissions);

                //Searching for buttons that are responsible for displaying specific pages.
                foreach (NavigationButtonTemplate nbt in this.NavigationBar.Children)
                {
                    foreach (PagesUserControls puc in listWithPermissions)
                    {
                        if (nbt.PageToOpen == puc)
                        {
                            nbt.Visibility = Visibility.Visible;
                            nbt.IsEnabled = true;
                            break;
                        }
                        else
                        {
                            nbt.Visibility = Visibility.Collapsed;
                            nbt.IsEnabled = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region Statc Methods
        /// <summary>
        /// Its looking for parent of object.
        /// </summary>
        /// <typeparam name="T">Type of parent.</typeparam>
        /// <param name="child">Child, current object.</param>
        /// <returns></returns>
        public static T FindParent<T> (DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
        #endregion
    }
}
