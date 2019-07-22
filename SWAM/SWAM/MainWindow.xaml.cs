using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SWAM.Controls.Pages;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Windows;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region Public statics
        /// <summary>
        /// Flag indicating whether the application is maximized.
        /// </summary>
        public static bool IsMaximized = false;
        /// <summary>
        /// Current user logged in to application.
        /// </summary>
        public static User LoggedInUser = new User
        {
            Id = 1,
            Name = "Admin",
            Permissions = UserType.Programmer
        };
        /// <summary>
        /// Flag indication that user is logged in or not.
        /// </summary>
        public static bool IsLoggedIn = true;
        /// <summary>
        /// Static instance of main window.
        /// </summary>
        private static MainWindow currentInstance;
        #endregion

        #region Properties
        /// <summary>
        /// Container with whole messageBoxes.
        /// </summary>
        public Dictionary<WindowType, Window> MessageBoxes = new Dictionary<WindowType, Window>()
        {
            { WindowType.Question, new ConfirmWindow()}
        };
        /// <summary>
        /// Indicates which page is currently loaded.
        /// </summary>
        private PagesUserControls _currentPageLoaded;
        /// <summary>
        /// Container with whole pages view.
        /// </summary>
        private Dictionary<PagesUserControls, UserControl> _pages = new Dictionary<PagesUserControls, UserControl>()
        {
            { PagesUserControls.LoginPage,new LoginPage()},
            { PagesUserControls.AdministratorPage, new AdministratorPage()},
            { PagesUserControls.ManageItemsPage, new ManageItemPage()},
            { PagesUserControls.ManageMagazinePage, new ManageMagazinePage() },
            { PagesUserControls.ManageOrdersPage, new ManageOrdersPage() },
            { PagesUserControls.LogedInUserProfile, new CurrentUserProfileTemplate(){ DataContext = LoggedInUser} },
            { PagesUserControls.MessagesPage, new MessagesPageTemplate() }

        };
        /// <summary>
        /// Container with whole priviligase of UserType.
        /// </summary>
        public static readonly Dictionary<UserType, List<PagesUserControls>> PAGES_FOR_USER = new Dictionary<UserType, List<PagesUserControls>>()
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
        public PagesUserControls CurrentPageLoaded { get => this._currentPageLoaded; private set => this._currentPageLoaded = value; }
        #endregion

        #region BasicConstructor
        public MainWindow()
        {
            InitializeComponent();

            ChangeContent(PagesUserControls.LoginPage);

            currentInstance = this;
        }
        #endregion
        
        #region Window Functions Buttons
        #region Maximize_Click
        /// <summary>
        /// Maximizing size of the application when the application size is smaller then max size. 
        /// Setting standard size of the application when the application is maximized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMaximized)
            {
                //Property IsMaximized must by first to current count of list in AdministratorPage-UsersList!!!!
                IsMaximized = true;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                //Property IsMaximized must by first to current count of list in AdministratorPage-UsersList!!!!
                IsMaximized = false;
                this.WindowState = WindowState.Normal;
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
        private void Exit_Click(object sender, RoutedEventArgs e) => this.Close();
        #endregion
        #endregion

        #region Change main content of the application
        /// <summary>
        /// Change current view of application to chosen one.
        /// </summary>
        /// <param name="page">The Page which should be loaded.</param>
        public void ChangeContent(PagesUserControls page)
        {
            if (this.ContentOfWindow.Children.Capacity > 0 && page != this._currentPageLoaded)
                this.ContentOfWindow.Children.RemoveAt(this.ContentOfWindow.Children.Count - 1);

            if (page != this._currentPageLoaded)
            {
                if (this._pages.TryGetValue(page, out UserControl pagetoAdd))
                {
                    this.ContentOfWindow.Children.Add(pagetoAdd);
                    this._currentPageLoaded = page;
                }
                else
                    this.ContentOfWindow.Children.Add(new SWAM.Controls.Pages.ErrorPage());
            }
        }
        #endregion

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

            foreach (NavigationButtonTemplate nvb in this.NavigationBar.Children)
            {
                if (this._currentPageLoaded == nvb.PageToOpen) nvb.IsSelected = true;
                else nvb.IsSelected = false;
            }
        }
        #endregion

        #region InformationForUser
        /// <summary>
        /// Information about action in bottomBar in main window.
        /// </summary>
        /// <param name="newInformation">Iformation for the user to be displayed</param>
        /// <param name="warning">If true - background red, false - background transparent</param>
        public void InformationForUser(string newInformation, bool warning = false)
        {
            InformationLabel.Text = newInformation;

            if (warning) InformationLabel.Background = (Brush)Application.Current.FindResource("WarningBrash"); 
            else InformationLabel.Background = Brushes.Transparent;
        }
        #endregion
        #region Statc Methods
        #region FindParent
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

            if (parentObject is T parent)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
        #endregion
        #region EnabledEverything
        /// <summary>
        /// Enable every controls in widnow.
        /// </summary>
        public static void EnabledEverything() => currentInstance.EverythingInWindow.IsEnabled = false;
        #endregion
        #region DisabledEverything
        /// <summary>
        /// Disable every control in windwo.
        /// </summary>
        public static void DisabledEverything() => currentInstance.EverythingInWindow.IsEnabled = true;
        #endregion
        #endregion
        #region SWAM_Closed
        /// <summary>
        /// Action after application is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SWAM_Closed(object sender, EventArgs e)
        {
            //close all open messageBoxes...
            foreach (KeyValuePair<WindowType, Window> entry in MessageBoxes)
                entry.Value.Close();
        }
        #endregion

        private void UserProfile_Click(object sender, RoutedEventArgs e) => ChangeContent(PagesUserControls.LogedInUserProfile);

        private void Messages_Click(object sender, RoutedEventArgs e) => ChangeContent(PagesUserControls.MessagesPage);
    }
}
