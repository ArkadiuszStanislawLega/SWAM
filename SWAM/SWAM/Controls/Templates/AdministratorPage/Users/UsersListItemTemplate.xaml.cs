using SWAM.Events.NavigationButton;
using SWAM.Exceptions;
using SWAM.Models;
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

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy UsersListItem.xaml
    /// </summary>
    public partial class UsersListItemTemplate : Button
    {
        public UsersListItemTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// After click button show Profile of user in UserControlPanelTemplate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userControlPanel = SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this);
            if (userControlPanel != null)
                userControlPanel.ShowProfile(this);
            else InformationToUser(ErrorMesages.DURING_CHANGING_USER_PROFILE_ERROR);
        }

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        protected bool InformationToUser(string message, bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(message, warning);
                    return true;
                }
                else throw new InformationLabelException(message);
            }
            catch (InformationLabelException ex)
            {
                ex.ShowMessage(this);
                return false;
            }
        }
        #endregion
    }
}
