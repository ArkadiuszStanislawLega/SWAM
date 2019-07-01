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
using System.Windows.Shapes;

namespace SWAM.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        /// <summary>
        /// User answer.
        /// </summary>
        bool _userAnswer = false;
    
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
        public void Show(string message, out bool answer)
        {
            this.Question.Text = message;
            //TODO: Debug this.
            try
            {
                this.ShowDialog();
            }
            catch(InvalidOperationException) { }

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
