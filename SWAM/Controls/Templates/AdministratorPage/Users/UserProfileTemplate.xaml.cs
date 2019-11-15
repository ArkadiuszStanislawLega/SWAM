using SWAM.Models.User;
using System.Windows;
using SWAM.Exceptions;
using SWAM.Strings;

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
            if (DataContext is User user)
            {
                if (this._confirmWindow != null)
                {
                    this._confirmWindow.Show($"Czy na pewno chcesz zablokować użytkownika {user.Name}?", out bool isConfirmed, "Potwierdź zablokowanie użykownika");
                    if (isConfirmed)
                    {
                        //its needed to clear the datacontext because the refresh function did not work properly after downloading the data after the change
                        this.DataContext = null;

                        if (user.StatusOfUserAccount == Enumerators.StatusOfUserAccount.Active) user.ChangeStatus(Enumerators.StatusOfUserAccount.Blocked);
                        else user.ChangeStatus(Enumerators.StatusOfUserAccount.Active);
                        InformationToUser($"Status konta użytkownika {user.Name} została zmieniony.");

                        this.DataContext = User.GetUser(user.Id);
                    }
                }
                else InformationToUser($"{ErrorMesages.DURING_CHANGING_STATUS_USER_ACCOUT_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_CHANGING_STATUS_USER_ACCOUT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
    }
}
