using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Messages;
using SWAM.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy MessagesPageTemplate.xaml
    /// </summary>
    public partial class MessagesPage : BasicUserControl
    {
        #region Properties
        /// <summary>
        /// Conatiner with messages.
        /// </summary>
        public MessagesListViewModel MessagesListViewModel { get; set; } = new MessagesListViewModel();
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
        public MessagesPage()
        {
            InitializeComponent();
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
                this._currentMessage = message;
                this.CurrentMessage.Text = message.ContentOfMessage;
                this.SenderName.Text = message.Sender.Name;
                this.DateOfSend.Text = $"\t{message.PostDate.ToString()}";
                this.TitleOfMessage.Text = message.TitleOfMessage;

                if(message.DateOfReading != null) this.DateOfReading.Text = $"\t{message.DateOfReading.ToString()}";

                if (this._isResivedIsOpen && !message.IsReaded)
                {
                    //set this message as "is readed"
                    SWAM.Models.Message.IsReadedToTrue(message.Id);
                    SWAM.Models.Message.SetDateOfReading(message.Id);

                    row.IsSelected = true;

                    //refresh number of unreaded messages in main window
                    SWAM.MainWindow.RefreshMessagesButton();
                }
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
            SendMessageWindow sendMessageWindow = new SendMessageWindow();
            sendMessageWindow.Tag = SWAM.MainWindow.MessagesWindows.Count;
            SWAM.MainWindow.MessagesWindows.Add(sendMessageWindow);
            SWAM.MainWindow.MessagesWindows[(int)sendMessageWindow.Tag].Show();
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
                SendMessageWindow sendMessageWindow = new SendMessageWindow();
                sendMessageWindow.Tag = SWAM.MainWindow.MessagesWindows.Count;
                sendMessageWindow.SendMessageReplay.SetReplayMessage(this._currentMessage);

                SWAM.MainWindow.MessagesWindows.Add(sendMessageWindow);
                SWAM.MainWindow.MessagesWindows[(int)sendMessageWindow.Tag].Show();
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
            RefreshMessagesList();

            //TODO: Think about it Why this don't Work in xaml
            MessagesList.ItemsSource = MessagesListViewModel.MessagesList;

            MessagesList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PostDate", System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion

        #region RefreshMessagesList
        /// <summary>
        /// Refresh data context depends on what context is shown by user.
        /// </summary>
        public void RefreshMessagesList()
        {
            if (this._isResivedIsOpen)
                SetReceivedContent();
            else
                SetSendedContent();
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
        private void ReceivedMessages_Click(object sender, RoutedEventArgs e) =>  SetReceivedContent();
        #endregion
        #region SetReceivedContent
        /// <summary>
        /// Change data context to Resived messages.
        /// </summary>
        private void SetReceivedContent()
        {
            MessagesListViewModel.RefreshResivedMessages(SWAM.MainWindow.LoggedInUser.Id);
            this.ContentTitle.Text = "Wiadomości odebrane";
            this._isResivedIsOpen = true;
        }
        #endregion
        #region SetSendedContent
        /// <summary>
        /// Change data context to Sended messages.
        /// </summary>
        private void SetSendedContent()
        {
            MessagesListViewModel.RefreshSendedMessages(SWAM.MainWindow.LoggedInUser.Id);
            this.ContentTitle.Text = "Wiadomości wysłane";
            this._isResivedIsOpen = false;
        }
        #endregion

        private void DeleteMessage_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MessagesList.Items.Count; i++)
            {
                FrameworkElement columnFromDataGrid = MessagesList.Columns[0].GetCellContent(MessagesList.Items[i]);
              
                if (columnFromDataGrid is CheckBox isDeletedChecked && isDeletedChecked.IsChecked == true && MessagesList.Items[i] is Message message)
                {
                    SWAM.Models.Message.DeleteMessage(message.Id);
                    RefreshMessagesList();
                }
            }
        }

        private void DeleteCurrentMessage_Click(object sender, RoutedEventArgs e)
        {
            SWAM.Models.Message.DeleteMessage(this._currentMessage.Id);
            RefreshMessagesList();
        }
    }
}
