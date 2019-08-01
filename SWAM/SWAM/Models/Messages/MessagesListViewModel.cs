using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SWAM.Models.Messages
{
    public class MessagesListViewModel : UserControl
    {
        private ObservableCollection<Message> _messagesListViewModel = new ObservableCollection<Message>();

        public ObservableCollection<Message> MessagesList { get => this._messagesListViewModel; }

        public void RefreshResivedMessages(int userId)
        {
            if(this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            foreach (Message message in Message.AllReceivedMessages(userId))
                this._messagesListViewModel.Add(message);
        }
        public void RefreshSendedMessages(int userId)
        {
            if (this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            foreach (Message message in Message.AllSendedMessages(userId))
                this._messagesListViewModel.Add(message);
        }
    }
}
