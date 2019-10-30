using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using SWAM.Enumerators;
using SWAM.Exceptions;
using SWAM.Models.User;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy loginPage.xaml
    /// </summary>
    public partial class LoginPage : BasicPage
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
            //Try to login in.
            var user = User.TryLogIn(UserLogin.Text, UserPassword.Password);

            if (user == null ) InformationToUser($"Błędny login lub hasło!", true); //If logging into the system is unsuccessful.
            else
            {
                if(MainWindow.currentInstance != null)
                    MainWindow.currentInstance.ChangeContent(PagesUserControls.MessagesPage);
            }

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
                if (SWAM.MainWindow.currentInstance != null)
                {
                    SWAM.MainWindow.currentInstance.InformationForUser(message, warning);
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
