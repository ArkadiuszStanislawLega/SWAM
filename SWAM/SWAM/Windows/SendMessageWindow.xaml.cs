using SWAM.Models;
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
    /// Logika interakcji dla klasy SendMessageWindow.xaml
    /// </summary>
    public partial class SendMessageWindow : Window
    {
        private bool _isMaximized;
        public SendMessageWindow()
        {
            InitializeComponent();
        }

        #region GetReplayMessage
        /// <summary>
        /// Sets all the values of the messages to which we respond. 
        /// </summary>
        /// <param name="message">The message to which the answer is written</param>
        public void SetReplayMessage(Message message)
        {
            this.ReceiverName.Text = message.Receiver.Name;
            this.FindUser.IsEnabled = false;
            this.Title.Text = $"Re:{message.TitleOfMessage}";
            this.Message.Text = $"\n\n--- Odpowiedź na wiadomość: ---\n{message.ContentOfMessage}\n \t--- Koniec wiadomości ---";
            this.Message.ScrollToHome();
        }
        #endregion

        #region Exit_Click
        /// <summary>
        /// Action after click in top bar right corner button.
        /// Closing application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e) => this.Close();
        #endregion
        #region Maximize_Click
        /// <summary>
        /// Maximizing size of the application when the application size is smaller then max size. 
        /// Setting standard size of the application when the application is maximized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (!this._isMaximized)
            {
                //Property IsMaximized must by first to current count of list in AdministratorPage-UsersList!!!!
                this._isMaximized = true;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                //Property IsMaximized must by first to current count of list in AdministratorPage-UsersList!!!!
                this._isMaximized = false;
                this.WindowState = WindowState.Normal;
            }
        }

        #endregion
        #region Minimize_Click
        /// <summary>
        /// Minimalizing size of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Window_Closed
        /// <summary>
        /// Action after this windows is closed.
        /// Removing window from the list with all opened windows.
        /// </summary>
        /// <param name="sender">This window.</param>
        /// <param name="e">Event windows is closed.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (SWAM.MainWindow.MessagesWindows.Count > 0) SWAM.MainWindow.MessagesWindows.RemoveAt((int)this.Tag);
        }
        #endregion
    }
}
