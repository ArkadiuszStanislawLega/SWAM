using SWAM.Events.NavigationButton;
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
            SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this).ShowProfile(this);
        }
    }
}
