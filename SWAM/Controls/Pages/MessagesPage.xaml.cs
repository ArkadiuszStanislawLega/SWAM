using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Messages;
using SWAM.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SWAM.Strings;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy MessagesPageTemplate.xaml
    /// </summary>
    public partial class MessagesPage : BasicPage
    {
        #region Properties
        /// <summary>
        /// Contains shown message values.
        /// </summary>
        private Message _currentMessage;
        /// <summary>
        /// Flag indicating whether received messages are on.
        /// </summary>
        private bool _isResivedIsOpen = true;
        #endregion
        #region Basic Constructor
        public MessagesPage() => InitializeComponent();
        #endregion

        #region SetDabuleClickedMessageContent
        /// <summary>
        /// Show values of message in message section.
        /// </summary>
        /// <param name="message">The message we want to display.</param>
        private void SetDabuleClickedMessageContent(Message message)
        {
            if (message != null)
            {
                this._currentMessage = message;
                this.CurrentMessage.Text = message.ContentOfMessage;
                this.SenderName.Text = message.Sender.Name;
                this.Receiver.Text = message.Receiver.Name;
                this.DateOfSend.Text = $"\t{message.PostDate.ToString()}";
                this.TitleOfMessage.Text = message.TitleOfMessage;

                if (message.DateOfReading != null) this.DateOfReading.Text = $"\t{message.DateOfReading.ToString()}";
            }
            else
            {
                this._currentMessage = null;
                this.CurrentMessage.Text = "";
                this.SenderName.Text = "";
                this.Receiver.Text = "";
                this.DateOfSend.Text = "";
                this.TitleOfMessage.Text = "";
                this.DateOfReading.Text = "";
            }
        }
        #endregion

        #region Row_DoubleClick
        /// <summary>
        /// Action after double click a row in data grid with messages.
        /// </summary>
        /// <param name="sender">Data grid row</param>
        /// <param name="e">Double click action</param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is Message message)
            {
                SetDabuleClickedMessageContent(message);

                if (this._isResivedIsOpen)
                {
                    SetReceivedContent();
                    if (!message.IsReaded)
                    {
                        //set this message as "is readed"
                        if (!SWAM.Models.Message.IsReadedToTrue(message))
                            InformationToUser("Wystąpił problem z zaznaczeniem wiadomości jako odczytane.", true);

                        if(!SWAM.Models.Message.SetDateOfReading(message))
                            InformationToUser("Wystąpił problem z nadaniem daty odczytania wiadomości.", true);

                        row.IsSelected = true;

                        //refresh number of unreaded messages in main window
                        SWAM.MainWindow.RefreshMessagesButton();
                    }
                }
                else
                    SetSendedContent();

            }
            else InformationToUser($"{ErrorMesages.MESSAGE_READ_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region NewMessage_Click
        /// <summary>
        /// Action after click new message button.
        /// </summary>
        /// <param name="sender">Button new message</param>
        /// <param name="e">Event click button</param>
        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.CurrentInstance.Windows.TryGetValue(Enumerators.WindowType.SendMessage, out Window sendMessageWindow))
            {
                sendMessageWindow.Show();
            }
        }
        #endregion
        #region ReplayMessage_Click
        /// <summary>
        /// Action after click replay message button.
        /// </summary>
        /// <param name="sender">Button replay message</param>
        /// <param name="e">Event click button</param>
        private void ReplayMessage_Click(object sender, RoutedEventArgs e)
        {
            if (this._currentMessage != null)
            {
                if (SWAM.MainWindow.CurrentInstance.Windows.TryGetValue(Enumerators.WindowType.SendMessage, out Window sendMessageWindow) &&
                    sendMessageWindow is SendMessageWindow messageWindow)
                {
                    messageWindow.SendMessageReplay.SetReplayMessage(this._currentMessage);
                    messageWindow.Show();
                }
            }
        }
        #endregion

        #region BasicUserControl_Loaded
        /// <summary>
        /// Action after this MessagesPage is loaded.
        /// </summary>
        /// <param name="sender">Current instance</param>
        /// <param name="e">Loaded</param>
        private void BasicUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();

            //TODO: Think about it Why this don't Work in xaml
            MessagesList.ItemsSource = MessagesListViewModel.Instance.MessagesList;

            MessagesList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PostDate", System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh data context depends on what context is shown by user.
        /// </summary>
        public void RefreshData()
        {
            if (this._isResivedIsOpen) SetReceivedContent();
            else SetSendedContent();
        }
        #endregion

        #region SendedMessages_Click
        /// <summary>
        /// Action after click sended messages buttons.
        /// Shows all messages which are sended by user.
        /// </summary>
        /// <param name="sender">Button show sended messages</param>
        /// <param name="e">Action clicked</param>
        private void SendedMessages_Click(object sender, RoutedEventArgs e) => SetSendedContent();
        #endregion
        #region ReceivedMessages_Click
        /// <summary>
        /// Action after click received messages buttons.
        /// Shows all messages which are received by user.
        /// </summary>
        /// <param name="sender">Button show received messages</param>
        /// <param name="e">Action clicked</param>
        private void ReceivedMessages_Click(object sender, RoutedEventArgs e) => SetReceivedContent();
        #endregion

        #region SetReceivedContent
        /// <summary>
        /// Change data context to Resived messages.
        /// </summary>
        private void SetReceivedContent()
        {
            if (SWAM.MainWindow.LoggedInUser != null)
            {
                MessagesListViewModel.Instance.RefreshResivedMessages();
                this.ContentTitle.Text = "Wiadomości odebrane";
                this._isResivedIsOpen = true;
            }
        }
        #endregion
        #region SetSendedContent
        /// <summary>
        /// Change data context to Sended messages.
        /// </summary>
        private void SetSendedContent()
        {
            MessagesListViewModel.Instance.RefreshSendedMessages();
            this.ContentTitle.Text = "Wiadomości wysłane";
            this._isResivedIsOpen = false;
        }
        #endregion

        #region DeleteMessage_Click
        /// <summary>
        /// Action after click delete message button.
        /// Delete message from user e-mail.
        /// </summary>
        /// <param name="sender">Delete message button</param>
        /// <param name="e">Action clicked</param>
        private void DeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MessagesList.Items.Count; i++)
            {
                FrameworkElement columnFromDataGrid = MessagesList.Columns[0].GetCellContent(MessagesList.Items[i]);

                if (columnFromDataGrid is CheckBox isDeletedChecked && isDeletedChecked.IsChecked == true && MessagesList.Items[i] is Message message)
                {
                    //If the user is browsing received messages...
                    if (this._isResivedIsOpen)
                        SWAM.Models.Message.DeleteMessageByReceiver(message);
                    else
                        SWAM.Models.Message.DeleteMessageBySender(message);

                    RefreshData();
                }
            }
        }
        #endregion
        #region DeleteCurrentMessage_Click
        /// <summary>
        /// Action after click delete current message button.
        /// Delete the currently viewed message.
        /// </summary>
        /// <param name="sender">Delete current message button</param>
        /// <param name="e">Action clicked</param>
        private void DeleteCurrentMessage_Click(object sender, RoutedEventArgs e)
        {
            //If the user is browsing received messages...
            if (this._currentMessage != null)
            {
                if (this._isResivedIsOpen)
                    SWAM.Models.Message.DeleteMessageByReceiver(this._currentMessage);
                else
                    SWAM.Models.Message.DeleteMessageBySender(this._currentMessage);

                SetDabuleClickedMessageContent(null);
                RefreshData();
            }
        }
        #endregion

        #region RefreshMessage_Click
        /// <summary>
        /// Action after click refresh button.
        /// Refresh current list of messages.
        /// </summary>
        /// <param name="sender">Refresh button</param>
        /// <param name="e">Action clicked</param>
        private void RefreshMessage_Click(object sender, RoutedEventArgs e) => RefreshData();
        #endregion
    }
}
