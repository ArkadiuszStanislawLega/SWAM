using SWAM.Models;
using SWAM.Models.Messages;
using SWAM.Models.User;
using SWAM.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.MessagesPage
{
    /// <summary>
    /// Logika interakcji dla klasy SendMessageTemplate.xaml
    /// </summary>
    public partial class SendMessageTemplate : UserControl
    {
        #region Properties
        /// <summary>
        /// Typed message
        /// </summary>
        public Message MessageToSend = new Message();
        /// <summary>
        /// List with messages that will be sent.
        /// </summary>
        private List<Message> _messagesList = new List<Message>();
        /// <summary>
        /// Flag indicating whether the window is in reply mode.
        /// </summary>
        bool _isReplayMessage = false;
        #endregion
        public SendMessageTemplate() => InitializeComponent();
        
        #region SetResceiver
        /// <summary>
        /// Sets the user who is the recipient of the message.
        /// </summary>
        /// <param name="receiver">Recipient of the message.</param>
        public void SetResceiver()
        {
            foreach (User user in SelectedUsersListViewModel.Instance.UsersList)
            {
                this._messagesList.Add(
                    new Message()
                    {
                        Sender = SWAM.MainWindow.LoggedInUser,
                        Receiver = user
                    });
            }
        }
        #endregion

        #region GetReplayMessage
        /// <summary>
        /// Sets all the values of the messages to which we respond. 
        /// </summary>
        /// <param name="message">The message to which the answer is written</param>
        public void SetReplayMessage(Message message)
        {
            this.MessageToSend.Receiver = message.Sender;
            this.MessageToSend.Sender = SWAM.MainWindow.LoggedInUser;
            SelectedUsersListViewModel.Instance.AddUser(message.Sender);//Meake receiver visibile in replay window.

            //Block adding more users.
            this.ChosenUserContainer.IsEnabled = false;
            this.FindUser.IsEnabled = false;

            //Copy message which are replayed.
            this.Title.Text = $"Re:{message.TitleOfMessage}";
            this.Message.Text = $"\n\n--- Odpowiedź na wiadomość: ---\n{message.ContentOfMessage}\n--- Koniec wiadomości ---";
            this.Message.ScrollToHome();

            this._isReplayMessage = true;
        }
        #endregion

        #region FindUser_Click
        /// <summary>
        /// Action after click find user button.
        /// </summary>
        /// <param name="sender">Find user button</param>
        /// <param name="e">Action clicked</param>
        private void FindUser_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow parent)
                parent.ChangeContent(Enumerators.BookmarkInPage.FindUserMessagesWindow);
        }
        #endregion
        #region SendMessage_Click
        /// <summary>
        /// Action after click send message button.
        /// Sending to database message, and closing current window.
        /// </summary>
        /// <param name="sender">Send message button</param>
        /// <param name="e">Action clicked.</param>
        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (this._isReplayMessage)
            {
                if (MessageToSend.Receiver != null)
                {
                    SetMessageValues(MessageToSend);

                    SWAM.Models.Message.AddMessage(MessageToSend);
                }
            }
            else
            {
                foreach (Message message in this._messagesList)
                    SetMessageValues(message);

                SWAM.Models.Message.AddManyMessages(this._messagesList);
            }

            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow sendMessageWindow)
                sendMessageWindow.Hide();

            SWAM.MainWindow.Instance.RefreshMessagesButton();
        }
        #endregion

        #region SetMessageValues
        /// <summary>
        /// Setting messages values, message content, title and post date.
        /// </summary>
        /// <param name="message">The message we want to supplement with the values.</param>
        private void SetMessageValues(Message message)
        {
            message.ContentOfMessage = this.Message.Text;
            message.TitleOfMessage = this.Title.Text;
            message.PostDate = DateTime.Now;
        }
        #endregion
        #region CancelSending_Click
        /// <summary>
        /// Action after click cancel send message button.
        /// Refresh parent window, to clear state and hide them.
        /// </summary>
        /// <param name="sender">Cancel send message button.</param>
        /// <param name="e">Event click.</param>
        private void CancelSending_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow sendMessageWindow)
            {
                sendMessageWindow.RefreshContents();
                sendMessageWindow.Hide();
            }
        }
        #endregion
    }
}
