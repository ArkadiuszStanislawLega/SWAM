using SWAM.Models;
using SWAM.Models.Messages;
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
        /// <summary>
        /// Typed message
        /// </summary>
        public Message MessageToSend = new Message();

        public SelectedUsersListViewModel SelectedUsersListViewModel { get; set; } = new SelectedUsersListViewModel();

        private List<Message> _messagesList = new List<Message>();

        bool _isReplayMessage = false;
        public SendMessageTemplate()
        {
            InitializeComponent();

            UsersList.DataContext = SelectedUsersListViewModel;
        }

        #region SetResceiver
        /// <summary>
        /// Sets the user who is the recipient of the message.
        /// </summary>
        /// <param name="receiver">Recipient of the message.</param>
        public void SetResceiver(SelectedUsersListViewModel receivers)
        {
            if (receivers != null && receivers.UsersList.Count > 0)
            {
                SelectedUsersListViewModel = receivers;

                foreach (User user in SelectedUsersListViewModel.UsersList)
                {
                    this._messagesList.Add(
                        new Message()
                        {
                            SenderId = SWAM.MainWindow.LoggedInUser.Id,
                            ReceiverId = user.Id
                        });
                }
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
            this.MessageToSend.ReceiverId = message.SenderId;
            this.MessageToSend.SenderId = SWAM.MainWindow.LoggedInUser.Id;
            this.FindUser.IsEnabled = false;
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
                if (MessageToSend.ReceiverId != 0)
                {
                    SetMessageValues(MessageToSend);

                    SWAM.Models.Message.AddMessage(MessageToSend);
                }
            }
            else
            {
                foreach(Message message in this._messagesList)
                    SetMessageValues(message);
                
                SWAM.Models.Message.AddManyMessages(this._messagesList);
            }

            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow sendMessageWindow)
                sendMessageWindow.Close();

            SWAM.MainWindow.RefreshMessagesButton();
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
    }
}
