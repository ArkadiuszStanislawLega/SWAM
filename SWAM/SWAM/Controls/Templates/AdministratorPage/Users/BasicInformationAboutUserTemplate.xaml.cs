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
    public partial class BasicInformationAboutUserTemplate : UserControl
    {
        /// <summary>
        /// Information about action.
        /// </summary>
        private string _message;

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

                this._message = $"Nazwa użytkownika {user.Name} została zmienione na {this.EditName.Text}.";
                try
                {
                    if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                        mainWindow.InformationForUser(this._message);
                    else throw new InformationLabelException(this._message);

                    if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this) is UserProfileTemplate userProfileTemplate)
                        userProfileTemplate.RefreshData();
                    else throw new RefreshUserProfileException($"{typeof(BasicInformationAboutUserTemplate).ToString()}\n");

                    if (SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) is UsersControlPanelTemplate usersControlPanelTemplate)
                        usersControlPanelTemplate.RefreshUsersList();
                    else throw new RefreshUserListException($"{typeof(BasicInformationAboutUserTemplate).ToString()}\n");

                }
                catch (InformationLabelException ex) { ex.ShowMessage(); }
                catch (RefreshUserProfileException ex) { ex.ShowMessage(); }
                catch (RefreshUserListException ex) { ex.ShowMessage(); }
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

                    #region Information for the user about the correctly carried out action
                    this._message = $"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}. ";
                    try
                    {
                        if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                            mainWindow.InformationForUser(this._message);
                        else throw new InformationLabelException(this._message);

                        if (SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) is UsersControlPanelTemplate usersControlPanelTemplate)
                            usersControlPanelTemplate.RefreshUsersList();
                        else throw new RefreshUserListException($"{typeof(BasicInformationAboutUserTemplate).ToString()}\n");
                    }
                    catch (InformationLabelException ex) { ex.ShowMessage(); }
                    catch (RefreshUserListException ex) { ex.ShowMessage(); }
                }
                #endregion
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

                    #region Information for the user about the correctly carried out action
                    this._message = $"Hasło użytkownika {user.Name} zostało zmienione.";
                    try
                    {
                        if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                            mainWindow.InformationForUser(this._message);
                        else throw new InformationLabelException(this._message);
                    }
                    catch (InformationLabelException ex) { ex.ShowMessage(); }
                    #endregion
                }
            }
            else
            {
                #region Information for the user about the incorrectly carried out action
                this._message = $"Hasła są niezgodne. Hasła muszą być takie same.";
                try
                {
                    if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                        mainWindow.InformationForUser(this._message);
                    else throw new InformationLabelException(this._message);
                }
                catch (InformationLabelException ex) { ex.ShowMessage(); }
                #endregion
            }
        }
        #endregion
    }
}
