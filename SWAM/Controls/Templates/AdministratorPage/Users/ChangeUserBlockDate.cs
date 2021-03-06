﻿using SWAM.Exceptions;
using SWAM.Models.User;
using SWAM.Strings;
using System;
using System.Windows.Input;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    public class ChangeUserBlockDate : CalendarWithButton
    {
        public ChangeUserBlockDate()
        {
            InitializeComponent();

            Loaded += ChangeUserBlockDate_Loaded;
        }

        #region ChangeUserBlockDate_Loaded
        /// <summary>
        /// Action after callendar is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeUserBlockDate_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is User user && user != null)
            {
                var date = user.ExpiryDateOfTheBlockade;
                if (date != null) this.Calendar.SelectedDate = User.GetUser(user.Id).ExpiryDateOfTheBlockade;
            }
        }
        #endregion
        #region NewCommand_Executed
        /// <summary>
        /// Action after click button "ok" when change date of user block.
        /// </summary>
        /// <param name="sender">Confrimation button.</param>
        /// <param name="e">Event clicked.</param>
        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Calendar.SelectedDate != null && this.Calendar.SelectedDate >= DateTime.Now)
            {
                if (DataContext is User user)
                {
                    user.ChangeExpiryDateOfTheBlockade(this.Calendar.SelectedDate);
                    UserProfileRefresh();

                    InformationToUserAsync($"Data blokady użytkownika {user.Name} została zmieniona na {this.Calendar.SelectedDate}.");
                }
                else
                    InformationToUserAsync($"{ErrorMesages.DATACONTEXT_ERROR}", true);
            }
            else
                InformationToUserAsync($"Popraw datę blokady użytkownika. Data nie może być wcześniejsza niż dzisiejsza.", true);
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
                else throw new RefreshUserProfileException($"{typeof(BasicInformationAboutUserTemplate).ToString()}");
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion
        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        private bool InformationToUserAsync(string message, bool warning = false)
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
    }
}
