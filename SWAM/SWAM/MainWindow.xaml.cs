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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private Pages _currentPageLoaded;
        /// <summary>
        /// Container of the main content of application.
        /// </summary>
        private StackPanel _pageContainer;
        #endregion

        #region Setters
        /// <summary>
        /// <seealso cref="_currentPageLoaded">
        /// </summary>
        public Pages CurrentPageLoaded { get => this._currentPageLoaded; set => this._currentPageLoaded = value; }
        #endregion

        #region BasicConstructor
        public MainWindow()
        {
            InitializeComponent();

            this._pageContainer = ContentOfWindow;
            ChangeContent(Pages.loginPage);
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
        public void ChangeContent(Pages page)
        {
            if(this._pageContainer.Children.Capacity>0)
            this._pageContainer.Children.RemoveAt(_pageContainer.Children.Count - 1);

            switch (page)
            {
                #region administratorPage
                case Pages.administratorPage:
                    {
                        if (_currentPageLoaded != Pages.administratorPage) _pageContainer.Children.Add(new AdministratorPage(this));
                        break;
                    }
                #endregion
                #region loginPage
                case Pages.loginPage:
                    {
                        if (_currentPageLoaded != Pages.loginPage) _pageContainer.Children.Add(new LoginPage(this));
                        break;
                    } 
                #endregion
                #region manageItemsPage
                case Pages.manageItemsPage:
                    {
                        if (_currentPageLoaded != Pages.manageItemsPage) this._pageContainer.Children.Add(new AdministratorPage(this));
                        break;
                    }
                #endregion
                #region manageMagazinePage
                case Pages.manageMagazinePage:
                    {
                        if (_currentPageLoaded != Pages.manageMagazinePage) _pageContainer.Children.Add(new AdministratorPage(this));
                        break;
                    }
                #endregion
                #region manageOrdersPage
                case Pages.manageOrdersPage:
                    {
                        if (_currentPageLoaded != Pages.manageOrdersPage)_pageContainer.Children.Add(new AdministratorPage(this));
                        break;
                    } 
                    #endregion
            }
        }
        #endregion
    }
}
