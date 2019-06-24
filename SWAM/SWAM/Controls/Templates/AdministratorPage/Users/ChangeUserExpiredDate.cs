using SWAM.Exceptions;
using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    class ChangeUserExpiredDate : CalendarWithButton
    {
        /// <summary>
        /// Infomration to user about actions.
        /// </summary>
        private string _message;

        public ChangeUserExpiredDate()
        {
            InitializeComponent();

            Loaded += ChangeUserExpiredDate_Loaded;
        }

        private void ChangeUserExpiredDate_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                //TODO: Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this.Calendar.SelectedDate = context.Users.FirstOrDefault(u => u.Id == user.Id).DateOfExpiryOfTheAccount;
                }
            }
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
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Users.FirstOrDefault(u => u.Id == user.Id).DateOfExpiryOfTheAccount = this.Calendar.SelectedDate;
                    context.SaveChanges();
                }

                UserProfileRefresh();
                this._message = $"Data wygaśnięcia konta {user.Name} została zmieniona na {this.Calendar.SelectedDate}.";
                InformationToUser();
            }
        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        private bool InformationToUser(bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(this._message, warning);
                    return true;
                }
                else throw new InformationLabelException(this._message);
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
                else throw new RefreshUserProfileException($"{typeof(BasicInformationAboutUserTemplate).ToString()}\n");
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
