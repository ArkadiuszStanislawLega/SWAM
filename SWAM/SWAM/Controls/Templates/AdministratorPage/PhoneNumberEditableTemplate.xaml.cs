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
    /// Logika interakcji dla klasy PhoneNumberEditableTemplate.xaml
    /// </summary>
    public partial class PhoneNumberEditableTemplate : UserControl
    {
        public PhoneNumberEditableTemplate()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.PhoneNumber.Visibility = Visibility.Collapsed;
            this.PhoneNumber.IsEnabled = false;

            this.EditPhoneNumber.Visibility = Visibility.Visible;
            this.EditPhoneNumber.IsEnabled = true;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
