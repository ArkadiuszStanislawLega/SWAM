using SWAM.Exceptions;
using SWAM.Models.User;
using SWAM.Strings;
using System;
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

        #region ChangeUserExpiredDate_Loaded
        /// <summary>
        /// Action when change user expire date is loaded.
        /// Set new data context.
        /// </summary>
        /// <param name="sender">Change user expire date.</param>
        /// <param name="e">Evenet is loaded.</param>
        private void ChangeUserExpiredDate_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is User user)
                    this.Calendar.SelectedDate = user.DateOfExpiryOfTheAccount;
        }
        #endregion       
        #region NewCommand_Executed
        /// <summary>
        /// Action after click confirm change user accout date of expire button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Calendar.SelectedDate != null && this.Calendar.SelectedDate >= DateTime.Now )
            {
                if (DataContext is User user)
                {
                    user.ChangeDateOfExpiryOfTheAccount(this.Calendar.SelectedDate);
                    UserProfileRefresh();
                    InformationToUser($"Data wygaśnięcia konta {user.Name} została zmieniona na {this.Calendar.SelectedDate}.");
                }
                else
                    InformationToUser($"{ErrorMesages.DATACONTEXT_ERROR}", true);
            }
            else
                InformationToUser($"Popraw datę wygaśnięcia konta. Data nie może być wcześniejsza niż dzisiejsza.", true);
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
