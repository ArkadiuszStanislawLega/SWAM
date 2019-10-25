using SWAM.Models.User;
using SWAM.Cryptography;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewUserTemplate.xaml
    /// </summary>
    public partial class CreateNewUserTemplate : BasicUserControl
    {
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
            //TODO: Make passwords and user name validation.
            if (this.NewUserName.Text != "" &&
                this.UserPassword.Password != "" &&
                this.ConfirmPassword.Password == this.UserPassword.Password &&
                this.UserPermissions.SelectedValue != null)
            {
                var hashPassword = new CryptoService();
                var passwordSalt = CryptoService.GenerateSalt();

                var user = new User()
                {
                    Name = this.NewUserName.Text,
                    Password = CryptoService.ComputeHash(this.UserPassword.Password, passwordSalt),
                    PasswordSalt = passwordSalt,
                    DateOfCreate = DateTime.Now,
                    Permissions = (Enumerators.UserType)this.UserPermissions.SelectedValue,
                    StatusOfUserAccount = this.AccountStatus.IsChecked ==
                                                        true ? Enumerators.StatusOfUserAccount.Active : Enumerators.StatusOfUserAccount.Blocked,
                    DateOfExpiryOfTheAccount = this.AccoutnExpireCallendar.SelectedDate
                };

                if (user != null)
                {
                    User.AddNewUser(user);
                    InformationToUser($"Dodano nowego {user.Permissions.ToString()} {user.Name}.");
                    UserListRefresh();
                }
                else InformationToUser($"Nie udało się dodać użytkownika {this.NewUserName.Text}.", true);

                RestartTextBoxes();
            }
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

        private async void BasicUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            await Unload();
        }

        private async Task Unload()
        {
            this.BeginStoryboard((Storyboard)this.FindResource("LoadedStory"));
            await Task.Delay(600);
        }
    }
}
