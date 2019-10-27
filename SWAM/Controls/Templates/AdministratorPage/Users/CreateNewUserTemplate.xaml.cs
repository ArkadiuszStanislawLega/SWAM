using SWAM.Models.User;
using SWAM.Cryptography;
using System;
using System.Windows;
using SWAM.Strings;

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
            //Validate name
            if (NameValidation(this.NewUserName.Text))
            {
                //Validate passwords
                if (this.ConfirmPassword.Password == this.UserPassword.Password)
                {
                    if (User.IsValidPassword(this.UserPassword.Password))
                    {
                        //Validate selected user type
                        if (this.UserPermissions.SelectedValue != null)
                        {
                            //Generate password salt.
                            var passwordSalt = CryptoService.GenerateSalt();

                            var user = new User()
                            {
                                Name = this.NewUserName.Text,
                                Password = CryptoService.ComputeHash(this.UserPassword.Password, passwordSalt),   //Hash the password
                                PasswordSalt = passwordSalt,
                                DateOfCreate = DateTime.Now,
                                Permissions = (Enumerators.UserType)this.UserPermissions.SelectedValue,
                                StatusOfUserAccount = this.AccountStatus.IsChecked ==
                                                                    true ? Enumerators.StatusOfUserAccount.Active : Enumerators.StatusOfUserAccount.Blocked,
                                DateOfExpiryOfTheAccount = this.AccoutnExpireCallendar.SelectedDate
                            };

                            //Try to add new user to database
                            if (user != null && user.IsAdd(user))
                            {
                                InformationToUser($"Dodano nowego {user.Permissions.ToString()} {user.Name}.");
                                UserListRefresh();
                                RestartTextBoxes();
                            }
                            else InformationToUser($"Nie udało się dodać użytkownika {this.NewUserName.Text}.", true);
                        }
                        else InformationToUser("Przed utworzniem konta musisz wybrać jego typ.");
                    }
                    else InformationToUser(ErrorMesages.PASSWORD_REQUIREMENT_ERROR, true);
                }
                else InformationToUser(ErrorMesages.PASSWORD_COMMPILANCE_ERROR, true);
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
        #region NameValidation
        /// <summary>
        /// They will check the user's name for length.
        /// </summary>
        /// <param name="name">Name to check.</param>
        /// <returns>True if the name meets the requirements.</returns>
        private bool NameValidation(string name)
        {
            //Check name - The name cannot be empty
            if (name != string.Empty)
            {
                char[] nameLength = name.ToCharArray();
                //the name must contain more than 3 letters
                if (nameLength.Length > SWAM.MainWindow.MIN_NAME_LEGTH)
                {
                    return true;
                }
                else InformationToUser($"Nazwa musi mieć więcej niż 3 litery.", true);
            }
            else InformationToUser($"Błędna nazwa.", true);

            return false;
        }
        #endregion
    }
}
