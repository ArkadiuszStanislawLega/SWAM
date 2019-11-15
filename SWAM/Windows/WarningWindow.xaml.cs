using System;
using System.Windows;

namespace SWAM.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public WarningWindow()
        {
            InitializeComponent();
        }
        #region Show
        /// <summary>
        /// Showing the warning window and disable main window.
        /// </summary>
        /// <param name="message">Warning for user.</param>
        /// <param name="title">Message in top title of the window</param>
        public void Show(string message,  string title = "Błąd!")
        {
            this.Warning.Text = message;
            this.TitleOfMainPanel.Text = title;

            try
            {
                if (!this.ShowActivated)
                    this.ShowDialog();
                else
                {
                    Hide();
                    ShowDialog();
                }
            }
            catch (InvalidOperationException) {  /*TODO: Debug this.*/ }

            MainWindow.DisabledEverything();
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
            MainWindow.EnabledEverything();
            this.Hide();
        }
        #endregion
    }
}
