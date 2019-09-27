﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
            //Try to login in.
            var user = User.TryLogIn(UserLogin.Text, UserPassword.Password);

            if (user == null ) InformationToUser($"Błędny login lub hasło!", true); //If logging into the system is unsuccessful.
            else
            {
                //if logging into the system is successful, hide page animation begin and after this...
                var story = (Storyboard)FindResource("UnloadedStory");
                story.Completed += (seender, ee) =>
                {
                    //... refresh mssages button ...
                    SWAM.MainWindow.RefreshMessagesButton();

                    //... change content to message page.
                    if (SWAM.MainWindow.FindParent<MainWindow>(this) is SWAM.MainWindow mainWindow)
                        mainWindow.ChangeContent(PagesUserControls.MessagesPage);
                };
                story.Begin();
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