using SWAM.Models;
using SWAM.Windows;
using System;
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
        public SendMessageTemplate()
        {
            InitializeComponent();
        }

        #region SetResceiver
        /// <summary>
        /// Sets the user who is the recipient of the message.
        /// </summary>
        /// <param name="receiver">Recipient of the message.</param>
        public void SetResceiver(User receiver)
        {
            if (receiver != null)
            {
                this.MessageToSend.ReceiverId = receiver.Id;
                this.ReceiverName.Text = receiver.Name;
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
            this.MessageToSend = message;
            this.ReceiverName.Text = message.Receiver.Name;
            this.FindUser.IsEnabled = false;
            this.Title.Text = $"Re:{message.TitleOfMessage}";
            this.Message.Text = $"\n\n--- Odpowiedź na wiadomość: ---\n{message.ContentOfMessage}\n--- Koniec wiadomości ---";
            this.Message.ScrollToHome();
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
            if (MessageToSend.ReceiverId != 0)
            {
                MessageToSend.SenderId = SWAM.MainWindow.LoggedInUser.Id;
                MessageToSend.ContentOfMessage = this.Message.Text;
                MessageToSend.TitleOfMessage = this.Title.Text;
                MessageToSend.PostDate = DateTime.Now;
                SWAM.Models.Message.AddMessage(MessageToSend);

                if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow sendMessageWindow)
                    sendMessageWindow.Close();

                SWAM.MainWindow.RefreshMessagesButton();
            }
        }
        #endregion
    }
}
