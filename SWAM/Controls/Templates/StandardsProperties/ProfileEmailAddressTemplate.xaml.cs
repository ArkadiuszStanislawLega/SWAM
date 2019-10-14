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

namespace SWAM.Controls.Templates.StandardsProperties
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileEmailAddressTemplate.xaml
    /// </summary>
    public partial class ProfileEmailAddressTemplate : UserControl
    {
        public ProfileEmailAddressTemplate()
        {
            InitializeComponent();
        }

        private void CancelChangeEmailAddress_Click(object sender, RoutedEventArgs e) => this.EditEmailAddress.Text = string.Empty;
    }
}
