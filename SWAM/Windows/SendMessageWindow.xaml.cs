using SWAM.Controls.Templates.MessagesPage;
using SWAM.Enumerators;
using SWAM.Models.Messages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy SendMessageWindow.xaml
    /// </summary>
    public partial class SendMessageWindow : Window
    {
        #region Properties
        /// <summary>
        /// A flag indicating whether the window is maximized
        /// </summary>
        private bool _isMaximized;
        /// <summary>
        /// Container with bookmarks in this window for navigation.
        /// </summary>
        private Dictionary<BookmarkInPage, UserControl> _contents = new Dictionary<BookmarkInPage, UserControl>()
        {
            {BookmarkInPage.SendMessageMessagesWindow, new SendMessageTemplate() },
            {BookmarkInPage.FindUserMessagesWindow, new FindUserTemplate() }
        };
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
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if(IsActive) this.Hide();
        }
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
        #region RefreshContents
        /// <summary>
        /// Refresh saved instance in main window.
        /// Required wan user click cancel send message.
        /// </summary>
        public void RefreshContents()
        {
            SWAM.MainWindow.CurrentInstance.Windows.Remove(WindowType.SendMessage);
            SWAM.MainWindow.CurrentInstance.Windows.Add(WindowType.SendMessage, new SendMessageWindow());

            SelectedUsersListViewModel.Instance.UsersList.Clear();
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
        #region SendMessageReplay
        /// <summary>
        /// Template in which user is answering for a message
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
    }
}
