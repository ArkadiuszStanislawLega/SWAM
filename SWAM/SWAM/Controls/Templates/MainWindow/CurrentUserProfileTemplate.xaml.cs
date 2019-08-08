using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using SWAM.Exceptions;
using SWAM.Controls.Templates.AdministratorPage;

namespace SWAM.Controls.Templates.MainWindow
{
    /// <summary>
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class CurrentUserProfileTemplate : BasicUserControl
    {
        public CurrentUserProfileTemplate()
        {
            InitializeComponent();
        }

        #region RefreshData
        /// <summary>
        /// Getting data from database and refresh profile.
        /// </summary>
        public void RefreshData()
        {
            try
            {
                DataContext = SWAM.MainWindow.LoggedInUser;
                /*
                if (this.DataContext is User user)
                {
                    //Clearing datacontext... is required for proper refresh of profile.
                    this.DataContext = null;
                    this.DataContext = User.GetUser(user.Id);
                    if(this.DataContext == null) throw new RefreshUserProfileException("RefreshData");
                }
                else throw new RefreshUserProfileException("RefreshData");*/
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion

        private void BasicUserControl_Loaded(object sender, RoutedEventArgs e) => RefreshData();
    }
}
