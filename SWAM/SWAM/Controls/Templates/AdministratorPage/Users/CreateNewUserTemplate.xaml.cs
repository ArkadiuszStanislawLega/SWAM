using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM;
using System.Security;
using SWAM.Exceptions;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewUserTemplate.xaml
    /// </summary>
    public partial class CreateNewUserTemplate : UserControl
    {
        /// <summary>
        /// Information to user about actions.
        /// </summary>
        string _message;

        public CreateNewUserTemplate()
        {
            InitializeComponent();
        }

        #region Comfirm_Click
        /// <summary>
        /// Action after click creat user button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comfirm_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Try - catch
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.Add(new User()
                {
                    Name = this.NewUserName.Text,
                    Password = this.UserPassword.Password,
                    DateOfCreate = DateTime.Now,
                    Permissions = (Enumerators.UserType)this.UserPermissions.SelectedValue,
                    StatusOfUserAccount = this.AccountStatus.IsChecked ==
                                                        true ? Enumerators.StatusOfUserAccount.Active : Enumerators.StatusOfUserAccount.Blocked,
                    DateOfExpiryOfTheAccount = this.AccoutnExpireCallendar.SelectedDate
                });
                context.SaveChanges();

                if (user != null)
                {
                    this._message = $"Dodano nowego {user.Permissions.ToString()} {user.Name}.";
                    InformationToUser();

                    UserListRefresh();
                }
                else
                {
                    this._message = $"Nie udało się dodać nużytkownika {this.NewUserName.Text}.";
                    InformationToUser(true);
                }
                RestartTextBoxes();
            }
        }
        #endregion

        #region UserListRefresh
        /// <summary>
        /// Refreshing user list.
        /// </summary>
        private void UserListRefresh()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) is UsersControlPanelTemplate usersControlPanelTemplate)
                    usersControlPanelTemplate.RefreshUsersList();
                else throw new RefreshUserListException($"{typeof(CreateNewUserTemplate).ToString()}\n");
            }
            catch (RefreshUserListException ex) { ex.ShowMessage(this); }
        }
        #endregion
        #region InformationToUser
        /// <summary>
        ///  Make information in MainWindow to user about action.
        /// </summary>
        /// <param name="warning">Tue - When content of inforamtion is warning.</param>
        private void InformationToUser(bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                    mainWindow.InformationForUser(this._message, warning);
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex) { ex.ShowMessage(this); }
        }
        #endregion

        #region RestartTextBoxes
        /// <summary>
        /// Reset textboxes and combobox after create new user.
        /// </summary>
        private void RestartTextBoxes()
        {
            this.NewUserName.Text = "";
            this.UserPassword.Password = "";
            this.ConfirmPassword.Password = "";
            this.UserPermissions.SelectedValue = 0;
        }
        #endregion
    }
}
