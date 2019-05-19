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
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class UserProfileTemplate : UserControl
    {
        public UserProfileTemplate()
        {
            InitializeComponent();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            this.Name.Visibility = Visibility.Collapsed;
            this.Password.Visibility = Visibility.Collapsed;
            this.Permissions.Visibility = Visibility.Collapsed;

            this.EditName.Visibility = Visibility.Visible;
            this.EditPassword.Visibility = Visibility.Visible;
            this.EditPassword.IsEnabled = true;
            this.EditConfirmPassword.Visibility = Visibility.Visible;
            this.EditConfirmPassword.IsEnabled = true;
            this.EditPermissions.Visibility = Visibility.Visible;
            this.ConfirmChanges.IsEnabled = true;
        }

        private void ConfirmChanges_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
