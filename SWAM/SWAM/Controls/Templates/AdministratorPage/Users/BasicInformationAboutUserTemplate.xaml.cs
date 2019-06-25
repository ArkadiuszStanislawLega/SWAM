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
                //TODO: Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    //TODO: Validation of user name.
                    context.Users.FirstOrDefault(u => u.Id == user.Id).Name = this.EditName.Text;
                    context.SaveChanges();
                }
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
                    //TODO: Try - catch
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions = userType;
                        context.SaveChanges();

                        Permissions.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.ToString();
                    }
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
                //TODO: Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    //TODO: Add hash funkction on password.
                    context.Users.FirstOrDefault(u => u.Id == user.Id).Password = this.EditConfirmPassword.Password;
                    context.SaveChanges();

                    //TODO: After add hash function and debug - delete this one line.
                    this.Password.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Password;
                    InformationToUser($"Hasło użytkownika {user.Name} zostało zmienione.");
                }
            }
            else InformationToUser($"Hasła są niezgodne. Hasła muszą być takie same.", true);
        }
        #endregion
    }
}
