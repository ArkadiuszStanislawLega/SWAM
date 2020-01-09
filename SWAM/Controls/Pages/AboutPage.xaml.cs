namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy AboutPage.xaml
    /// </summary>
    public partial class AboutPage : BasicPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SWAM.MainWindow.Instance.ChangeContent(Enumerators.PagesUserControls.LoginPage);
        }
    }
}
