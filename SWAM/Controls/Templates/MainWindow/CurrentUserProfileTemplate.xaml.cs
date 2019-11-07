using System.Windows;
using SWAM.Exceptions;
using SWAM.Controls.Pages;
using SWAM.Models.User;

namespace SWAM.Controls.Templates.MainWindow
{
    /// <summary>
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class CurrentUserProfileTemplate : BasicPage
    {
        public CurrentUserProfileTemplate()
        {
            InitializeComponent();
        }

        #region RefreshData
        /// <summary>
        /// Getting data from database and refresh profile.
        /// </summary>
        public override void RefreshData()
        {
            try
            {
                DataContext = SWAM.MainWindow.LoggedInUser;
              
                if (this.DataContext is User user)
                {
                    //Clearing datacontext... is required for proper refresh of profile.
                    this.DataContext = null;
                    this.DataContext = User.GetUser(user.Id);
                    if(this.DataContext == null) throw new RefreshUserProfileException("RefreshData");
                }
                else throw new RefreshUserProfileException("RefreshData");
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion

        private void BasicUserControl_Loaded(object sender, RoutedEventArgs e) => RefreshData();
    }
}
