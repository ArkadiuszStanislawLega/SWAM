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
using System.Windows.Shapes;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        #region Properties
        new PagesUserControls NAME_OF_PAGE = PagesUserControls.administratorPage;

        /// <summary>
        /// Instances of all controlers in AdministratorPage.
        /// </summary>
        UserControl[] _userControls = {
            new WarehousesControlPanel(),
            new UsersControlPanel()
        };
        /// <summary>
        /// Current visible controler.
        /// </summary>
        UserControl _currentContent;
        #endregion

        #region Basic Constructor
        public AdministratorPage(MainWindow mainWindow) 
            : base(mainWindow)
        {
            InitializeComponent();

            _currentContent = _userControls[0];
            SwitchToUsersControlPanel.Background = this.FindResource("LightGrayBrash") as Brush;
        }
        #endregion

        #region Overrided Methods
        override protected void ChangePage_Click(object sender, RoutedEventArgs e) => this._mainWindow.ChangeContent(PagesUserControls.loginPage);
        #endregion

        private void WarhousesControlPanelBarPage_Click(object sender, RoutedEventArgs e) => ChangeContext(this._userControls[0]);
        private void UsersControlPanelBar_Click(object sender, RoutedEventArgs e) => ChangeContext(this._userControls[1]);

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
                MainContent.Children.RemoveAt(MainContent.Children.Capacity - 1);
                MainContent.Children.Add(this._currentContent);
            }

            if (this._currentContent == _userControls[0]) {
                SwitchToWarehousesControlPanel.Background = this.FindResource("LightGrayBrash") as Brush;
                SwitchToUsersControlPanel.Background = this.FindResource("MainBarBrash") as Brush;
            } 
            else
            {
                SwitchToWarehousesControlPanel.Background = this.FindResource("MainBarBrash") as Brush;
                SwitchToUsersControlPanel.Background = this.FindResource("LightGrayBrash") as Brush;
            }
        }
        #endregion  
    }
}
