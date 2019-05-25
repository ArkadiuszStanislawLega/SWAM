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
using SWAM.Controls.Templates.AdministratorPage;
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
        /// Instances of all controlers in AdministratorPage.
        /// </summary>
        UserControl[] _userControls = {
            new WarehousesControlPanelTemplate(),
            new UsersControlPanelTemplate()
        };

        /// <summary>
        /// Current visible controler.
        /// </summary>
        UserControl _currentContent;
        #endregion

        #region Basic Constructor
        public AdministratorPage() 
        {
            InitializeComponent();
        }
        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this._currentContent = this._userControls[1];
            ChangeButtonsBackground();
        }

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
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Capacity - 1);
                this.MainContent.Children.Add(this._currentContent);

                ChangeButtonsBackground();
            }
        }
        #endregion
        #region ChangeButtonsBackground
        /// <summary>
        /// Changing the colors of buttons that change the content of the page between WarehousesControlPanel and UsersControlPanel.
        /// </summary>
        private void ChangeButtonsBackground()
        {
            if (this._currentContent == this._userControls[0])
            {
                this.SwitchToWarehousesControlPanel.Background = this.FindResource("SelectedBrash") as Brush;
                this.SwitchToUsersControlPanel.Background = this.FindResource("EnabledBrash") as Brush;
            }
            else
            {
                this.SwitchToWarehousesControlPanel.Background = this.FindResource("EnabledBrash") as Brush;
                this.SwitchToUsersControlPanel.Background = this.FindResource("SelectedBrash") as Brush;
            }
        }
        #endregion
    }
}
