using SWAM.Enumerators;
using SWAM.Exceptions;
using SWAM.Windows;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy BasicUserControl.xaml
    /// </summary>
    public partial class BasicUserControl : UserControl
    {
        /// <summary>
        /// Main window instance.
        /// </summary>
        /// <returns></returns>
        protected SWAM.MainWindow _mainWindow { get => SWAM.MainWindow.FindParent<SWAM.MainWindow>(this); }
        /// <summary
        /// Confirm window instance.
        /// </summary>
        /// <returns>Confirm window.</returns>
        protected ConfirmWindow _confirmWindow { get => this._mainWindow.Windows.TryGetValue(WindowType.Question, out Window messageWindow) ? messageWindow as ConfirmWindow : null; }

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        protected bool InformationToUser(string message, bool warning = false)
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
        protected void UserProfileRefresh()
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
        #region UserListRefresh
        /// <summary>
        /// Refreshing user list.
        /// </summary>
        protected void UserListRefresh()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) is UsersControlPanelTemplate usersControlPanelTemplate)
                    usersControlPanelTemplate.RefreshUsersList();
                else throw new RefreshUserListException();
            }
            catch (RefreshUserListException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}

