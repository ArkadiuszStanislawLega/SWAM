using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM.Controls.Pages;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Models.User;
using SWAM.Windows;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        //TODO: Delete this in released 
        /// <summary>
        /// Temporary user id for debug.
        /// </summary>
        private const int TEMPORARY_USER_ID = 1;

        #region Public statics
        /// <summary>
        /// Current user logged in to application.
        /// </summary>
        public static User LoggedInUser { get; private set; } = new User
        {
            Id = TEMPORARY_USER_ID,
            Name = "Admin",
            Permissions = UserType.Programmer,
            //Messages = Message.AllReceivedMessages(TEMPORARY_USER_ID)
        };
        /// <summary>
        /// Visibile mode after user logged in or logged out.
        /// </summary>
        public static readonly DependencyProperty LoggedInUserStatus = DependencyProperty.Register(nameof(VisibleMode), typeof(Visibility), typeof(MainWindow));
        /// <summary>
        /// Flag indicating whether the application is maximized.
        /// </summary>
        public static bool IsMaximized = false;
        /// <summary>
        /// Static instance of main window.
        /// </summary>
        public static MainWindow currentInstance { get; private set; }
        #endregion

        #region Properties
        /// <summary>
        /// Visibile mode after user logged in or logged out.
        /// All user controls that appear only when a user is logged in should be connected to this property.
        /// </summary>
        public Visibility _visibleMode;
        /// <summary>
        /// Visibile mode after user logged in or logged out.
        /// All user controls that appear only when a user is logged in should be connected to this property.
        /// </summary>
        public Visibility VisibleMode
        {
            get => this._visibleMode;
            set
            {
                this._visibleMode = value;
                this.SetValue(LoggedInUserStatus, this._visibleMode);
            }
        }
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
        private readonly Dictionary<PagesUserControls, UserControl> _pages = new Dictionary<PagesUserControls, UserControl>()
        {
            { PagesUserControls.LoginPage,new LoginPage()},
            { PagesUserControls.AdministratorPage, new AdministratorPage()},
            { PagesUserControls.ManageItemsPage, new ManageItemPage()},
            { PagesUserControls.ManageMagazinePage, new ManageMagazinePage() },
            { PagesUserControls.ManageOrdersPage, new ManageOrdersPage() },
            { PagesUserControls.LogedInUserProfile, new CurrentUserProfileTemplate() },
            { PagesUserControls.MessagesPage, new MessagesPage() }
        };
        /// <summary>
        /// Container with whole priviligase of UserType.
        /// </summary>
        public static readonly Dictionary<UserType, List<PagesUserControls>> PAGES_FOR_USER = new Dictionary<UserType, List<PagesUserControls>>()
        {
           //Settings for administrator
           { UserType.Administrator,
                new List<PagesUserControls>(){ PagesUserControls.AdministratorPage }},
           //Settings for Seller
           { UserType.Seller,
                new List<PagesUserControls>(){ PagesUserControls.ManageMagazinePage }},
           //Setting for Waegouseman
           { UserType.Warehouseman,
                new List<PagesUserControls>(){ 
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

        public static List<SendMessageWindow> MessagesWindows = new List<SendMessageWindow>();
        #endregion

        #region BasicConstructor
        public MainWindow()
        {

            InitializeComponent();

            ChangeContent(PagesUserControls.LoginPage);

            currentInstance = this;

            RefreshMessagesButton();
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

            //If page what we want change is not loaded..
            if (page != this._currentPageLoaded)
            {
                //find page in dictionary
                if (this._pages.TryGetValue(page, out UserControl pageToAdd))
                {
                    if (page == PagesUserControls.LogedInUserProfile)
                    {
                        pageToAdd = new CurrentUserProfileTemplate
                        {
                            DataContext = MainWindow.LoggedInUser
                        };
                    }

                    this.ContentOfWindow.Children.Add(pageToAdd);

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

            //Mark the currently pressed navigation button as pressed and the others not pressed
            //Its depends on current main widow content
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
            //close all opened Windows
            foreach (KeyValuePair<WindowType, Window> entry in MessageBoxes)
                entry.Value.Close();
            foreach (SendMessageWindow messagesPageTemplate in MessagesWindows)
                messagesPageTemplate.Close();
        }
        #endregion

        #region UserProfile_Click
        /// <summary>
        /// Action after click user profile button. 
        /// </summary>
        /// <param name="sender">User profile button</param>
        /// <param name="e">Action clicked</param>
        private void UserProfile_Click(object sender, RoutedEventArgs e) => ChangeContent(PagesUserControls.LogedInUserProfile);
        #endregion
        #region Messages_Click
        /// <summary>
        /// Action after click messages button.
        /// </summary>
        /// <param name="sender">Messages button</param>
        /// <param name="e">Action clicked</param>
        private void Messages_Click(object sender, RoutedEventArgs e)
        {
            if (this._currentPageLoaded != PagesUserControls.MessagesPage)
                ChangeContent(PagesUserControls.MessagesPage);
            else
            {
                if(_pages.TryGetValue(PagesUserControls.MessagesPage, out UserControl userControl) && userControl is MessagesPage messagePage)
                    messagePage.RefreshMessagesList();
            }
        }
        #endregion
        #region LoginOut_Click
        /// <summary>
        /// Action after click menu item login out.
        /// </summary>
        /// <param name="sender">Menu item</param>
        /// <param name="e">Clicked</param>
        private void LoginOut_Click(object sender, RoutedEventArgs e) => SetLoggedInUser(null);
        #endregion

        #region RefreshMessagesButton
        /// <summary>
        /// Getting number of user unread messages, and update content of button.
        /// </summary>
        public static void RefreshMessagesButton()
        {
            throw new NotImplementedException();
            //if (LoggedInUser != null)
            //{
            //    int number = Message.CountUnreadedMessages(LoggedInUser.Id);
            //    currentInstance.Messages.Content = number > 0 ? $"{number}" : "";
            //}
        }
        #endregion

        #region RefreshNavigationButtons
        /// <summary>
        /// Refreshing all navigation buttons. Changing visibility of the buttons depends on type of currently logged in user account.
        /// </summary>
        public void RefreshNavigationButtons()
        {
            foreach(FrameworkElement u in NavigationBar.Children)
            {
                if (u is NavigationButtonTemplate button)
                    button.CheckIsVisible();   
            }
        }
        #endregion

        #region SetLoggedInUser
        /// <summary>
        /// Setting LoggedInUser. 
        /// Change visibility mode to all controls which required known is user logged in.
        /// Informing user about status of logged account in application.
        /// </summary>
        /// <param name="user">New user account instance to be set as the currently logged in account.</param>
        /// <returns>Currently logged in user instance.</returns>
        public static User SetLoggedInUser(User user)
        {
            LoggedInUser = user;

            if (LoggedInUser != null)
            {
                if (user.StatusOfUserAccount == StatusOfUserAccount.Blocked)
                    currentInstance.InformationForUser("Twoje konto jest zablokowane, zgłoś się do administratora systemu.", true);
                else
                {
                    currentInstance.VisibleMode = Visibility.Visible;
                    currentInstance.RefreshNavigationButtons();
                    currentInstance.InformationForUser($"Witaj w systemie {LoggedInUser.Name}.");
                }
            }
            else
            {
                currentInstance.VisibleMode = Visibility.Collapsed;
                currentInstance.InformationForUser("Wylogowano z systemu.");
                currentInstance.ChangeContent(PagesUserControls.LoginPage);
            }

            return LoggedInUser;
        }
        #endregion
    }
}
