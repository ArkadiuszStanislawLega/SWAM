using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM.Controls.Pages;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.Messages;
using SWAM.Models.User;
using SWAM.Windows;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region Constances
        /// <summary>
        /// Minimum courier name size.
        /// </summary>
        public const int MIN_NAME_LENGTH = 3;
        /// <summary>
        /// Time in miliseconds after which the messages are to be refreshed again. 
        /// Currently default value is 5 min.
        /// </summary>
        private const int REFRESHING_DELAY_OF_USER_MESSAGES = 300000;
        //TODO: Delete this in released 
        /// <summary>
        /// Temporary user id for debug.
        /// </summary>
        private const int TEMPORARY_USER_ID = 1;
        #endregion
        #region Private Statics
        private static User loggedInUser = new User()
        {
            Id = TEMPORARY_USER_ID,
            Name = "Admin",
            Permissions = UserType.Programmer
        };
        #endregion
        #region Public statics
        /// <summary>
        /// Current user logged in to application.
        /// </summary>
        public static User LoggedInUser
        {
            get => loggedInUser;
            private set
            {
                if (value == null)
                {
                    Instance.VisibleMode = Visibility.Collapsed;
                    Instance.InformationForUser("Wylogowano z systemu.");
                    Instance.ChangeContent(PagesUserControls.LoginPage);
                }
                loggedInUser = value;
            }
        }
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
        public static MainWindow Instance { get; private set; }
        #endregion

        #region Properties
        /// <summary>
        /// Warning window instance.
        /// </summary>
        public WarningWindow WarningWindow 
        {
            get
            {
                if (Windows.TryGetValue(WindowType.Warning, out Window window) && window is WarningWindow warningWindow)
                {
                    return warningWindow;
                }
                else
                    return null;
            }
        }
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
        public Dictionary<WindowType, Window> Windows = new Dictionary<WindowType, Window>()
        {
            { WindowType.Question, new ConfirmWindow()},
            { WindowType.SendMessage, new SendMessageWindow()},
            { WindowType.Warning, new WarningWindow()}
        };
        /// <summary>
        /// Indicates which page is currently loaded.
        /// </summary>
        private PagesUserControls _currentPageLoaded;
        /// <summary>
        /// Container with whole pages view.
        /// </summary>
        private readonly Dictionary<PagesUserControls, BasicPage> _pages = new Dictionary<PagesUserControls, BasicPage>()
        {
            { PagesUserControls.LoginPage,new LoginPage()},
            { PagesUserControls.AdministratorPage, new AdministratorPage()},
            { PagesUserControls.ManageItemsPage, new ManageItemPage()},
            { PagesUserControls.ManageMagazinePage, new ManageMagazinePage() },
            { PagesUserControls.ManageOrdersPage, new ManageOrdersPage() },
            { PagesUserControls.LogedInUserProfile, new CurrentUserProfileTemplate() },
            { PagesUserControls.MessagesPage, new MessagesPage() },
            { PagesUserControls.ManageCustomersPage, new ManageCustomersPage() },
            { PagesUserControls.ManageCouriersPage, new ManageCouriersPage() },
            { PagesUserControls.ManageExternalSuppliersPage, new ManageExternalSuppliersPage() }
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
                new List<PagesUserControls>(){ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage,
                                          /**/ PagesUserControls.ManageCustomersPage }},
           //Setting for Waegouseman
           { UserType.Warehouseman,
                new List<PagesUserControls>(){ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage }},
            { UserType.Manager,
                new List<PagesUserControls>(){ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage,
                                          /**/ PagesUserControls.ManageCustomersPage,
                                          /**/ PagesUserControls.ManageCouriersPage,
                                          /**/ PagesUserControls.ManageExternalSuppliersPage}},
            { UserType.Owner,
                new List<PagesUserControls>(){ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage,
                                          /**/ PagesUserControls.ManageCustomersPage,
                                          /**/ PagesUserControls.ManageCouriersPage,
                                          /**/ PagesUserControls.ManageExternalSuppliersPage}},
            { UserType.Programmer,
                new List<PagesUserControls>(){ PagesUserControls.LoginPage,
                                          /**/ PagesUserControls.AdministratorPage,
                                          /**/ PagesUserControls.ManageItemsPage,
                                          /**/ PagesUserControls.ManageMagazinePage,
                                          /**/ PagesUserControls.ManageOrdersPage,
                                          /**/ PagesUserControls.ManageCustomersPage,
                                          /**/ PagesUserControls.ManageCouriersPage,
                                          /**/ PagesUserControls.ManageExternalSuppliersPage}},
        };
        #endregion

        #region BasicConstructor
        public MainWindow()
        {
            InitializeComponent();

            SetUnloadStoryToAllPages();
            Instance = this;
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
            //If page what we want change is not loaded..
            if (page != this._currentPageLoaded)
            {
                //find page in dictionary
                if (this._pages.TryGetValue(page, out BasicPage pageToAdd))
                {
                    if (page == PagesUserControls.LogedInUserProfile)
                    {
                        pageToAdd = new CurrentUserProfileTemplate
                        {
                            DataContext = MainWindow.LoggedInUser
                        };
                    }
                    BasicPage.CurrentLoadedBasicPage.Add(pageToAdd);

                    if (BasicPage.CurrentLoadedBasicPage.Count > 1)
                    {
                        if (this._pages.TryGetValue(this._currentPageLoaded, out BasicPage pagetToUnload))
                        {
                            pagetToUnload.CreateStory();

                            Grid mainContent = new Grid();

                            switch (this._currentPageLoaded)
                            {
                                case PagesUserControls.AdministratorPage:
                                    {
                                        var currentPage = (AdministratorPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.EmptyPage:
                                    {
                                        var currentPage = (ErrorPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }

                                case PagesUserControls.LoginPage:
                                    {
                                        var currentPage = (LoginPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageCouriersPage:
                                    {
                                        var currentPage = (ManageCouriersPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageCustomersPage:
                                    {
                                        var currentPage = (ManageCustomersPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageExternalSuppliersPage:
                                    {
                                        var currentPage = (ManageExternalSuppliersPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageItemsPage:
                                    {
                                        var currentPage = (ManageItemPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageMagazinePage:
                                    {
                                        var currentPage = (ManageMagazinePage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.ManageOrdersPage:
                                    {
                                        var currentPage = (ManageOrdersPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                                case PagesUserControls.MessagesPage:
                                    {
                                        var currentPage = (MessagesPage)pagetToUnload;
                                        mainContent = currentPage.MainContent;
                                        break;
                                    }
                            }
                            pagetToUnload.SetStoryboardTarget(mainContent);
                            this.BeginStoryboard(pagetToUnload.UnloadStory);
                        }
                    }
                    else
                    {
                        this.ContentOfWindow.Children.Add(pageToAdd);
                    }

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
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
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
        public static void EnabledEverything() => Instance.EverythingInWindow.IsEnabled = false;
        #endregion
        #region DisabledEverything
        /// <summary>
        /// Disable every control in windwo.
        /// </summary>
        public static void DisabledEverything() => Instance.EverythingInWindow.IsEnabled = true;
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
            foreach (KeyValuePair<WindowType, Window> entry in Windows)
                entry.Value.Close();
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
                if (_pages.TryGetValue(PagesUserControls.MessagesPage, out BasicPage userControl) && userControl is MessagesPage messagePage)
                    messagePage.RefreshData();
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
        public void RefreshMessagesButton()
        {
            if (LoggedInUser != null)
            {
                int number = Message.CountUnreadedMessages(LoggedInUser);
                Instance.Messages.Content = number > 0 ? $"{number}" : "";
            }
        }
        #endregion

        #region RefreshNavigationButtons
        /// <summary>
        /// Refreshing all navigation buttons. Changing visibility of the buttons depends on type of currently logged in user account.
        /// </summary>
        public void RefreshNavigationButtons()
        {
            foreach (FrameworkElement u in Instance.NavigationBar.Children)
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
                

            }
            else
            {

            }

            return LoggedInUser;
        }
        #endregion
        #region SetUnloadStoryToAllPages
        /// <summary>
        /// Setting to all main pages unload storyboard and action after storyboard.
        /// </summary>
        private void SetUnloadStoryToAllPages()
        {
            foreach (KeyValuePair<PagesUserControls, BasicPage> entry in this._pages)
            {
                entry.Value.UnloadStory.Completed += (sender, e) =>
                {
                    this.ContentOfWindow.Children.Remove(entry.Value);
                    this.ContentOfWindow.Children.Add(BasicPage.CurrentLoadedBasicPage[1]);
                    BasicPage.CurrentLoadedBasicPage.RemoveAt(0);

                    if (entry.Key == PagesUserControls.LoginPage)
                    {
                        RefreshMessagesButton();
                    }
                };
            }
        }
        #endregion

        #region async Task RefreshMessageButton
        /// <summary>
        /// Refreshing message button every 5 min and message page if its opened.
        /// </summary>
        /// <returns>Asynchronous refreshing message task.</returns>
        public async Task RefreshMessageButton()
        {
            while (true)
            {
                //update message button
                RefreshMessagesButton();

                //if message page is opened
                if(this._currentPageLoaded == PagesUserControls.MessagesPage)
                {
                    //Get the message page
                    if(this._pages.TryGetValue(PagesUserControls.MessagesPage, out BasicPage currentPage) && currentPage is MessagesPage messagePage)
                    {
                        //if resived meessage bookmark is opened, refresh resived messages 
                        if (messagePage.IsResivedIsOpen)
                            MessagesListViewModel.Instance.RefreshResivedMessages();
                        //else refresh sended messages 
                        else
                            MessagesListViewModel.Instance.RefreshSendedMessages();
                    }
                }

                // don't run again for at least value of REFRESHING_DELAY_OF_USER_MESSAGES
                await Task.Delay(REFRESHING_DELAY_OF_USER_MESSAGES);
            }
        }
        #endregion

        #region RefreshDataOnPage
        /// <summary>
        /// Refreshing data depends on <see cref="_currentPageLoaded"/> property.
        /// </summary>
        private void RefreshDataOnPage()
        {
            switch (this._currentPageLoaded)
            {
                #region AdministratorPage
                case PagesUserControls.AdministratorPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.AdministratorPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region ManageCouriersPage
                case PagesUserControls.ManageCouriersPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageCouriersPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region ManageCustomersPage
                case PagesUserControls.ManageCustomersPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageCustomersPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region ManageExternalSuppliersPage
                case PagesUserControls.ManageExternalSuppliersPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageExternalSuppliersPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region ManageItemsPage
                case PagesUserControls.ManageItemsPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageItemsPage, out BasicPage currentPage))
                             currentPage.RefreshData();
                        break;
                    }
                #endregion
                #region ManageMagazinePage
                case PagesUserControls.ManageMagazinePage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageMagazinePage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region ManageOrdersPage
                case PagesUserControls.ManageOrdersPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.ManageOrdersPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region MessagesPage
                case PagesUserControls.MessagesPage:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.MessagesPage, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                #endregion
                #region LogedInUserProfile
                case PagesUserControls.LogedInUserProfile:
                    {
                        if (this._pages.TryGetValue(PagesUserControls.LogedInUserProfile, out BasicPage currentPage))
                        {
                            currentPage.RefreshData();
                        }
                        break;
                    }
                    #endregion

            }
        }
        #endregion 
        #region RefereshData_Click
        /// <summary>
        /// Action after pressing the context menu button "refresh data" at the top of the application.
        /// </summary>
        /// <param name="sender">Context menu button "refresh data".</param>
        /// <param name="e">Event clicked.</param>
        private void RefereshData_Click(object sender, RoutedEventArgs e) => RefreshDataOnPage();
        #endregion
        #region RefreshDataCommand_Executed
        /// <summary>
        /// Action after click button F5 in application.
        /// Refreshing the lists depending on the enabled page.
        /// </summary>
        /// <param name="sender">Button F5</param>
        /// <param name="e">Event button click.</param>
        private void RefreshDataCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => RefreshDataOnPage();
        #endregion
    }
}
