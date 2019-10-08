using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.Messages
{
    public class MessagesListViewModel : UserControl
    {
        private ObservableCollection<Message> _messagesListViewModel = new ObservableCollection<Message>();

        public ObservableCollection<Message> MessagesList { get => this._messagesListViewModel; }

        #region Singletone Pattern
        static MessagesListViewModel() => Instance.RefreshResivedMessages();

        private static readonly MessagesListViewModel _instance = new MessagesListViewModel();
        public static MessagesListViewModel Instance => _instance;
        #endregion

        public void RefreshResivedMessages()
        {
            if(this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            foreach (Message message in Message.AllReceivedMessages(SWAM.MainWindow.LoggedInUser.Id))
                this._messagesListViewModel.Add(message);
        }
        public void RefreshSendedMessages()
        {
            if (this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            foreach (Message message in Message.AllSendedMessages(SWAM.MainWindow.LoggedInUser.Id))
                this._messagesListViewModel.Add(message);
        }
    }
}
