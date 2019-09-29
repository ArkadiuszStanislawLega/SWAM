using SWAM.Exceptions;
using SWAM.Models.User;
using System.Windows.Input;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    class ChangeUserExpiredDate : CalendarWithButton
    {
        public ChangeUserExpiredDate()
        {
            InitializeComponent();

            Loaded += ChangeUserExpiredDate_Loaded;
        }

        private void ChangeUserExpiredDate_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is User user)
                    this.Calendar.SelectedDate = user.DateOfExpiryOfTheAccount;
        }

        #region NewCommand_Executed
        /// <summary>
        /// Action after click confirm change user accout date of expire button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO: Create validation.
            if (this.Calendar.SelectedDate != null && DataContext is User user)
            {
                user.ChangeDateOfExpiryOfTheAccount(this.Calendar.SelectedDate);
                UserProfileRefresh();
                InformationToUser($"Data wygaśnięcia konta {user.Name} została zmieniona na {this.Calendar.SelectedDate}.");
            }
        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        private bool InformationToUser(string message, bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(message, warning);
                    return true;
                }
                else throw new InformationLabelException(message);
            }
            catch (InformationLabelException ex)
            {
                ex.ShowMessage(this);
                return false;
            }
        }
        #endregion
        #region UserProfileRefresh
        /// <summary>
        /// Refresh current user profile.
        /// </summary>
        private void UserProfileRefresh()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this) is UserProfileTemplate userProfileTemplate)
                    userProfileTemplate.RefreshData();
                else throw new RefreshUserProfileException();
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
