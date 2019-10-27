using SWAM.Models.User;
using System.Windows;
using SWAM.Strings;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy BasicInformationAboutUserTemplate.xaml
    /// </summary>
    public partial class BasicInformationAboutUserTemplate : BasicUserControl
    {
        /// <summary>
        /// Name before edit.
        /// </summary>
        private string _name;

        public BasicInformationAboutUserTemplate()
        {
            InitializeComponent();
        }
    
        #region ConfirmChangeName_Click
        /// <summary>
        /// Action after click confrim change user name button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmChangeName_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                user.ChangeName(this.EditName.Text);
                InformationToUser($"Nazwa użytkownika {user.Name} została zmienione na {this.EditName.Text}.");
                UserProfileRefresh();
                UserListRefresh();
            }
            else InformationToUser(ErrorMesages.DURING_EDIT_USER_ERROR, true);
        }
        #endregion
        #region ConfirmEditPermission_Click
        /// <summary>
        /// Action after click confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmEditPermission_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                var userType = (Enumerators.UserType)this.EditPermissions.SelectedValue;

                if (userType != user.Permissions)
                {
                    user.ChangePermissions(userType);
                    Permissions.Text = User.GetUser(user.Id).Permissions.ToString();

                    InformationToUser($"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}. ");
                    UserListRefresh();

                    this.EditPermissions.SelectedValue = null;
                }
                else InformationToUser("Uprawnienia są tego samego typu. Nie zostało nic zmienione.");
            }
            else InformationToUser(ErrorMesages.DURING_EDIT_USER_ERROR, true);
        }
        #endregion
        #region ConfirmEditPasswordButton_Click
        /// <summary>
        /// Action after click confirm user password change button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmEditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (User.IsValidPassword(this.EditPassword.Password))
            {
                if (this.EditPassword.Password == this.EditConfirmPassword.Password && DataContext is User user)
                {
                    //In exceptional cases or older created accounts situations password salt can be null.
                    //So when password salt is null, we need to create new one.
                    if (user.PasswordSalt == null)
                        user.PasswordSalt = Cryptography.CryptoService.GenerateSalt();

                    var newPassword = Cryptography.CryptoService.ComputeHash(this.EditConfirmPassword.Password, user.PasswordSalt);

                    if (newPassword != new byte[0])
                    {
                        user.ChangePassword(newPassword);
                        InformationToUser($"Hasło użytkownika {user.Name} zostało zmienione.");
                    }
                    else
                        //This error may appear if the account password salt was generated incorrectly when creating the account.
                        InformationToUser($"{ErrorMesages.DURING_EDIT_PASSWORD_ERROR}");
                }
                else InformationToUser($"{ErrorMesages.PASSWORD_COMMPILANCE_ERROR}", true);
            }
            else InformationToUser(ErrorMesages.PASSWORD_REQUIREMENT_ERROR, true);
        }
        #endregion

        #region CancelChangeName_Click
        /// <summary>
        /// Action after click cancel change name button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChangeName_Click(object sender, RoutedEventArgs e) => this.EditName.Text = this._name;
        #endregion
        #region CancelEditPermission_Click
        /// <summary>
        /// Acton after click cancel edit permisions during editting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEditPermission_Click(object sender, RoutedEventArgs e) => EditPermissions.SelectedValue = null;
        #endregion
        #region CancelEditPasswordButton_Click
        /// <summary>
        /// Action after click cancel edit password button during editting password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            this.EditPassword.Password = "";
            this.EditConfirmPassword.Password = "";
        }
        #endregion

        #region EditNameOfUserButton_Click
        /// <summary>
        /// Action after click edit user name button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditNameOfUserButton_Click(object sender, RoutedEventArgs e)=> this._name = this.UserName.Text;
        #endregion
    }
}
