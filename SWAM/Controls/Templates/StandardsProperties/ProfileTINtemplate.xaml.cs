using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.StandardsProperties
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileTINtemplate.xaml
    /// </summary>
    public partial class ProfileTINtemplate : UserControl
    {
        public ProfileTINtemplate()
        {
            InitializeComponent();
        }

        private void CancelChangeTIN_Click(object sender, RoutedEventArgs e) => this.EditTin.Text = string.Empty;
    }
}
