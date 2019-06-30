using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using SWAM.Exceptions;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class UserProfileTemplate : BasicUserControl
    {
        public UserProfileTemplate()
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
                if (this.DataContext is User user)
                {
                    //Clearing datacontext... is required for proper refresh of profile.
                    this.DataContext = null;

                    var refreshedUser = User.GetUser(user.Id);
                    if (refreshedUser != null)
                        this.DataContext = refreshedUser;
                    else throw new RefreshUserProfileException("RefreshData");
                }
                else throw new RefreshUserProfileException("RefreshData");
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion  
        #region BlockUser_Click
        /// <summary>
        /// Action after click button block user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make a window asking if you really want to block this user.
            if (DataContext is User user)
            {
                //its needed to clear the datacontext because the refresh function did not work properly after downloading the data after the change
                this.DataContext = null;
                if (user.StatusOfUserAccount == Enumerators.StatusOfUserAccount.Active) user.ChangeStatus(Enumerators.StatusOfUserAccount.Blocked);
                else user.ChangeStatus(Enumerators.StatusOfUserAccount.Active);
                InformationToUser($"Status konta użytkownika {user.Name} została zmieniony.");

                this.DataContext = User.GetUser(user.Id);
            }
            else InformationToUser(ErrorMesages.DURING_CHANGING_STATUS_USER_ACCOUT_ERROR, true);
        }
        #endregion
    }
}
