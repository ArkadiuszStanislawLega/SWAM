using SWAM.Controls.Templates.Messages;
using SWAM.Enumerators;
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
        #region Properties
        private bool _isMaximized;

        private Dictionary<BookmarkInPage, UserControl> _contents = new Dictionary<BookmarkInPage, UserControl>()
        {
            {BookmarkInPage.SendMessageMessagesWindow, new SendMessageTemplate() },
            {BookmarkInPage.FindUserMessagesWindow, new FindUserTemplate() }
        };

        /// <summary>
        /// Template in which I answer for a message 
        /// </summary>
        public SendMessageTemplate SendMessageReplay
        {
            get
            {
                if (this._contents.TryGetValue(BookmarkInPage.SendMessageMessagesWindow, out UserControl userControl) 
                    && userControl is SendMessageTemplate sendMessageTemplate)
                    return sendMessageTemplate;

                else return null;
            }
        }
        #endregion

        public SendMessageWindow()
        {
            InitializeComponent();
            ChangeContent(BookmarkInPage.SendMessageMessagesWindow);
        }

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

        #region ChangeContent
        /// <summary>
        /// Changing main content of this window.
        /// </summary>
        /// <param name="content"><see cref="BookmarkInPage"/> new content of the window</param>
        public void ChangeContent(BookmarkInPage content)
        {
            if (this.Content.Children.Count > 0)
            {
                this.Content.Children.RemoveAt(this.Content.Children.Count - 1);

                if (this._contents.TryGetValue(content, out UserControl userControl))
                    this.Content.Children.Add(userControl);
            }
            else if (this._contents.TryGetValue(content, out UserControl userControl))
                this.Content.Children.Add(userControl);
        }
        #endregion
    }
}
