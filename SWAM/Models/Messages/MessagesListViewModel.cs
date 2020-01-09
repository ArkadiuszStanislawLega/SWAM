using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.Messages
{
    /// <summary>
    /// List view model messages that the user has received or sent depending on which page is enabled.
    /// </summary>
    public class MessagesListViewModel : UserControl
    {
        private ObservableCollection<Message> _messagesListViewModel = new ObservableCollection<Message>();
        public ObservableCollection<Message> MessagesList { get => this._messagesListViewModel; }

        #region Singletone Pattern
        static MessagesListViewModel() => Instance.RefreshResivedMessages();

        private static readonly MessagesListViewModel _instance = new MessagesListViewModel();
        public static MessagesListViewModel Instance => _instance;
        #endregion
        #region RefreshResivedMessages
        /// <summary>
        /// Refreshes the list and retrieves messages that the user has received from the database.
        /// </summary>
        public void RefreshResivedMessages()
        {
            if(this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            if (MainWindow.LoggedInUser != null && MainWindow.LoggedInUser.Id > 0)
            {
                foreach (Message message in Message.AllReceivedMessages(SWAM.MainWindow.LoggedInUser.Id))
                    this._messagesListViewModel.Add(message);
            }
        }
        #endregion
        #region RefreshSendedMessages
        /// <summary>
        /// Refreshes the list and retrieves messages that the user has send from the database.
        /// </summary>
        public void RefreshSendedMessages()
        {
            if (this._messagesListViewModel.Count > 0) this._messagesListViewModel.Clear();

            foreach (Message message in Message.AllSendedMessages(SWAM.MainWindow.LoggedInUser.Id))
                this._messagesListViewModel.Add(message);
        }
        #endregion
    }
}
