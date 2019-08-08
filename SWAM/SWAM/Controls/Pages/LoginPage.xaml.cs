using System.Windows;
using System.Windows.Controls;
using SWAM.Enumerators;
using SWAM.Exceptions;
using SWAM.Models;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy loginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()       
        {
            InitializeComponent();
        }
        #region LoginButton_Click
        /// <summary>
        /// Action after click login in button.
        /// Validaton of login and password.
        /// </summary>
        /// <param name="sender">Login in button</param>
        /// <param name="e">Action clicked</param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = User.TryLogIn(UserLogin.Text, UserPassword.Password);

            if (user != null )
            {
                if (user.StatusOfUserAccount == StatusOfUserAccount.Active)
                {
                    InformationToUser($"Witaj { SWAM.MainWindow.LoggedInUser.Name}!");

                    SWAM.MainWindow.FindParent<MainWindow>(this).LoginInMode();
                }
                else InformationToUser("Twoje konto jest zablokowane, zgłoś się do administratora systemu.", true);
            }
            else InformationToUser($"Błędny login lub hasło!", true);

            UserPassword.Password = "";
        }
        #endregion
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
    }
}
