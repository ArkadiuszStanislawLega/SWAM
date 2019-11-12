using System;
using System.Windows;

namespace SWAM.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        #region Properties
        /// <summary>
        /// User answer.
        /// </summary>
        bool _userAnswer = false;
        #endregion

        public ConfirmWindow()
        {
            InitializeComponent();
        }
        #region Show
        /// <summary>
        /// Showing the confirm window and disable main window.
        /// </summary>
        /// <param name="message">Question for user, what action it's needed confirmation</param>
        /// <param name="answer">Reference to property where answare is required</param>
        /// <param name="title">Message in top title of the window</param>
        public void Show(string message, out bool answer, string title ="Czy jesteś pewien?")
        {
            this.Question.Text = message;
            this.TitleOfMainPanel.Text = title;

            try
            {
                if(!this.ShowActivated)
                    this.ShowDialog();
                else
                { 
                    Hide();
                    ShowDialog();
                }
            }
            catch(InvalidOperationException) {  /*TODO: Debug this.*/ }

            answer = this._userAnswer;

            MainWindow.DisabledEverything();
        }
        #endregion
        #region Yes_Click
        /// <summary>
        /// Action after click Yes button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = true;
            EverythingIsEnabledAndWindowHide();
        }
        #endregion
        #region No_Click
        /// <summary>
        /// Action after click No button.
        /// </summary>
        private void No_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = false;
            EverythingIsEnabledAndWindowHide();
        }
        #endregion
        #region EverythingIsEnabledAndWindowHide
        /// <summary>
        /// Make Stack panel in main window Enabled
        /// </summary>
        private void EverythingIsEnabledAndWindowHide()
        {
            MainWindow.EnabledEverything();
            this.Hide();
        }
        #endregion
        #region Exit_Click
        /// <summary>
        /// Action after click in top bar right corner button.
        /// Closing application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this._userAnswer = false;
            MainWindow.EnabledEverything();
            this.Hide();
        }
        #endregion
    }
}
