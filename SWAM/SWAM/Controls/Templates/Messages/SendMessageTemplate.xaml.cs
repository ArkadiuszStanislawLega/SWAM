using SWAM.Models;
using SWAM.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWAM.Controls.Templates.Messages
{
    /// <summary>
    /// Logika interakcji dla klasy SendMessageTemplate.xaml
    /// </summary>
    public partial class SendMessageTemplate : UserControl
    {
        public Message MessageToSend = new Message();
        public SendMessageTemplate()
        {
            InitializeComponent();
        }

        public void SetResceiver(User receiver)
        {
            if (receiver != null)
            {
                this.MessageToSend.ReceiverId = receiver.Id;
                this.ReceiverName.Text = receiver.Name;
            }
        }

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

        private void FindUser_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow parent)
                parent.ChangeContent(Enumerators.BookmarkInPage.FindUserMessagesWindow);
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (MessageToSend.ReceiverId != 0)
            {
                MessageToSend.SenderId = SWAM.MainWindow.LoggedInUser.Id;
                MessageToSend.ContentOfMessage = this.Message.Text;
                MessageToSend.TitleOfMessage = this.Title.Text;
                MessageToSend.PostDate = DateTime.Now;
                SWAM.Models.Message.AddMessage(MessageToSend);
            }
        }
    }
}
