using System;
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
            if (User.TryLogIn(UserLogin.Text, UserPassword.Password) && MainWindow.CurrentInstance != null)
                    MainWindow.CurrentInstance.ChangeContent(PagesUserControls.MessagesPage);
            

            this.UserPassword.Password = "";
        }
        #endregion
    }
}
