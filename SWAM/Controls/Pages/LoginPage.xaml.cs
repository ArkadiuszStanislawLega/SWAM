using System;
using System.Threading.Tasks;
using System.Windows;
using SWAM.Enumerators;
using SWAM.Models.User;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy loginPage.xaml
    /// </summary>
    public partial class LoginPage : BasicPage
    {
        #region Properties
        /// <summary>
        /// The number of failed attempts to log into the system.
        /// </summary>
        private int _failedLogingAttempts = 0;
        #endregion
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
        private async void  LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Try to login in.
            if (User.TryLogIn(UserLogin.Text, UserPassword.Password) && MainWindow.Instance != null)
            {
                MainWindow.Instance.ChangeContent(PagesUserControls.MessagesPage);
                this._failedLogingAttempts = 0;
                await MainWindow.Instance.RefreshMessageButton();
            }
            else
            {
                this._failedLogingAttempts++;
                if (this._failedLogingAttempts >= MainWindow.MAX_FAILED_LOGING_ATTEMPTS)
                    this.LoginButton.IsEnabled = false;
                else
                {
                    switch (this._failedLogingAttempts)
                    {
                        case 1:
                            await LoginDelay(MainWindow.FIRST_ATTEMPT_DELAY);
                            break;
                        case 2:
                            await LoginDelay(MainWindow.SECOND_ATTEMPT_DELAY);
                            break;
                    }
                }
            }
            
            this.UserPassword.Password = "";
        }
        #endregion

        #region LoginDelay
        /// <summary>
        /// Block the login button for a specified time.
        /// </summary>
        /// <param name="delay">Time period for which the button is to be blocked.</param>
        /// <returns></returns>
        public async Task LoginDelay(int delay)
        {
            this.LoginButton.IsEnabled = false;
            int counter = delay;
            await Task.Run(async () =>
            {
                while (counter > 0 )
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.LoginButton.Visibility = Visibility.Collapsed;
                        this.TimeCounterView.Text = $"Pozostało prób: {MainWindow.MAX_FAILED_LOGING_ATTEMPTS - this._failedLogingAttempts} ({counter}s)";
                    }));
                    await Task.Delay(1000);
                    counter--;

                    if (counter == 0)
                    {
                        Dispatcher.Invoke(new Action(() => {
                            this.LoginButton.Visibility = Visibility.Visible;
                            this.TimeCounterView.Text = string.Empty;
                        }))
                       ;
                    }
                }
            });
            this.LoginButton.IsEnabled = true;
        }
        #endregion

    }
}
