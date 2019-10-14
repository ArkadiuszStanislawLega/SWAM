using System.Windows;
using System.Windows.Controls;


namespace SWAM.Controls.Templates.StandardsProperties
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileNameTemplate.xaml
    /// </summary>
    public partial class ProfileNameTemplate : UserControl
    {
        public ProfileNameTemplate()
        {
            InitializeComponent();
        }
        private void CancelChangeName_Click(object sender, RoutedEventArgs e) => this.EditName.Text = string.Empty;
        
    }
}
