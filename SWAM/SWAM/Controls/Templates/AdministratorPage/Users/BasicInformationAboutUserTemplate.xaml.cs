using SWAM.Exceptions;
using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (this.EditPassword.Password == this.EditConfirmPassword.Password && DataContext is User user)
            {
                //TODO: Add hash function on password.
                user.ChangePassword(this.EditConfirmPassword.Password);
                InformationToUser($"Hasło użytkownika {user.Name} zostało zmienione.");
                //TODO: Delete this after add hash function.
                this.Password.Text = User.GetUser(user.Id).Password;
            }
            else InformationToUser($"Hasła są niezgodne. Hasła muszą być takie same.", true);
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
