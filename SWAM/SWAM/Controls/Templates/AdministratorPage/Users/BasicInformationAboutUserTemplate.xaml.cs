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
        public BasicInformationAboutUserTemplate()
        {
            InitializeComponent();
        }

        #region EditNameCommand_Executed
        /// <summary>
        /// Action after click confrim change user name button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditNameCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                user.ChangeName(this.EditName.Text);
                InformationToUser($"Nazwa użytkownika {user.Name} została zmienione na {this.EditName.Text}.");
                UserProfileRefresh();
                UserListRefresh();
            }
        }
        #endregion 
        #region EditUserPermissionsCommand_Executed
        /// <summary>
        /// Action after clock confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUserPermissionsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                var userType = (Enumerators.UserType)this.EditPermissions.SelectedValue;

                if (userType != user.Permissions)
                {
                    user.ChangePermissions(userType);
                    Permissions.Text = user.Permissions.ToString();

                    //TODO: Try - catch
                    using (var context = new ApplicationDbContext())
                        Permissions.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.ToString();
                    
                    InformationToUser($"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}. ");
                    UserListRefresh();
                }
            }
        }
        #endregion
        #region EditUserPasswordCommand_Executed
        /// <summary>
        /// Action after click confirm user password change button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUserPasswordCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.EditPassword.Password == this.EditConfirmPassword.Password && DataContext is User user)
            {
                //TODO: Add hash funkction on password.
                user.ChangePassword(this.EditConfirmPassword.Password);
                InformationToUser($"Hasło użytkownika {user.Name} zostało zmienione.");

                //TODO: Try - catch - DELETE this after add hash function to password.
                using (ApplicationDbContext context = new ApplicationDbContext())
                    this.Password.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Password;
            }
            else InformationToUser($"Hasła są niezgodne. Hasła muszą być takie same.", true);
        }
        #endregion
    }
}
