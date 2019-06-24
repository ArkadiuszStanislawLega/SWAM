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
    public class ChangeUserBlockDate : CalendarWithButton
    {
        /// <summary>
        /// Information to user about actions.
        /// </summary>
        private string _message;

        public ChangeUserBlockDate()
        {
            InitializeComponent();

            Loaded += ChangeUserBlockDate_Loaded;
        }

        private void ChangeUserBlockDate_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is User user && user != null)
            {
                //TODO: Try-catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var date = context.Users.FirstOrDefault(u => u.Id == user.Id).ExpiryDateOfTheBlockade;
                    if (date != null) this.Calendar.SelectedDate = (context.Users.FirstOrDefault(u => u.Id == user.Id).ExpiryDateOfTheBlockade);
                }
            }
        }

        #region NewCommand_Executed
        /// <summary>
        /// Action after click button "ok" when change date of user block.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO: Create validation.
            if (this.Calendar.SelectedDate != null && DataContext is User user)
            {
                //TODO: Try-catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Users.FirstOrDefault(u => u.Id == user.Id).ExpiryDateOfTheBlockade = this.Calendar.SelectedDate;
                    context.SaveChanges();
                }
                UserProfileRefresh();

                this._message = $"Data blokady użytkownika {user.Name} została zmieniona na {this.Calendar.SelectedDate}.";
                InformationToUser();
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

    }
}
