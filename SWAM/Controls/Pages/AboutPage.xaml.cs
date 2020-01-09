using System.Windows;

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

        /// <summary>
        /// Action after click back button.
        /// Change main content of SWAM to login page.
        /// </summary>
        /// <param name="sender">Back button.</param>
        /// <param name="e">Event click.</param>
        private void Back_Click(object sender, System.Windows.RoutedEventArgs e) => MainWindow.Instance.ChangeContent(Enumerators.PagesUserControls.LoginPage);
        
        /// <summary>
        /// Action after change visibilty of current user control.
        /// </summary>
        /// <param name="sender">About page.</param>
        /// <param name="e">Event changing page visibility.</param>
        private void BasicPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => this.BackButton.Visibility = MainWindow.LoggedInUser == null ? Visibility.Visible : Visibility.Hidden;
        
    }
}
