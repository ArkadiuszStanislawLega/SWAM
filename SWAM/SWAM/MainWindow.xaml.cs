using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SWAM.Enumerators;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        }
        #endregion  

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
            if(!this._IsMaximized)
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

        #region ChangeContent
        /// <summary>
        /// Change current view of application to chosen one.
        /// </summary>
        /// <param name="page">The Page which should be loaded.</param>
        public void ChangeContent(PagesUserControls page)
        {
            if(this._pageContainer.Children.Capacity>0) this._pageContainer.Children.RemoveAt(_pageContainer.Children.Count - 1);

            switch (page)
            {
                #region administratorPage
                case PagesUserControls.AdministratorPage:
                    {
                        if (_currentPageLoaded != PagesUserControls.AdministratorPage) _pageContainer.Children.Add(new AdministratorPage(this));
                        break;
                    }
                #endregion
                #region loginPage
                case PagesUserControls.LoginPage:
                    {
                        if (_currentPageLoaded != PagesUserControls.LoginPage) _pageContainer.Children.Add(new LoginPage(this));
                        break;
                    } 
                #endregion
                #region manageItemsPage
                case PagesUserControls.ManageItemsPage:
                    {
                        if (_currentPageLoaded != PagesUserControls.ManageItemsPage) this._pageContainer.Children.Add(new ManageItemPage(this));
                        break;
                    }
                #endregion
                #region manageMagazinePage
                case PagesUserControls.ManageMagazinePage:
                    {
                        if (_currentPageLoaded != PagesUserControls.ManageMagazinePage) _pageContainer.Children.Add(new ManageMagazinePage(this));
                        break;
                    }
                #endregion
                #region manageOrdersPage
                case PagesUserControls.ManageOrdersPage:
                    {
                        if (_currentPageLoaded != PagesUserControls.ManageOrdersPage)_pageContainer.Children.Add(new ManageOrdersPage(this));
                        break;
                    } 
                    #endregion
            }
        }
        #endregion

        #region TemporaryNaviagionBar_Click
        /// <summary>
        /// Temporary navigation bar - buttonClicker.
        /// It is for fast navigation between pages, for debug and project phase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemporaryNaviagionBar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Content)
            {
                case "administratorPage":{ ChangeContent(PagesUserControls.AdministratorPage); break; }
                case "loginPage": { ChangeContent(PagesUserControls.LoginPage); break; }
                case "manageItemsPage": { ChangeContent(PagesUserControls.ManageItemsPage); break; }
                case "manageMagazinePage": { ChangeContent(PagesUserControls.ManageMagazinePage); break; }
                case "manageOrdersPage": { ChangeContent(PagesUserControls.ManageOrdersPage); break; }
            }
        }
        #endregion
    }
}
