using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Messages;
using SWAM.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public MessagesListViewModel MessagesListViewModel { get; set; } = new MessagesListViewModel();

        private Message _currentMessage;
        public MessagesPage()
        {
            InitializeComponent();
        }

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

                if (!message.IsReaded)
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

        private void BasicUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshMessagesList();

            //TODO: Think about it Why this don't Work in xaml
            MessagesList.ItemsSource = MessagesListViewModel.MessagesList;

            MessagesList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PostDate", System.ComponentModel.ListSortDirection.Descending));
        }

        public void RefreshMessagesList() => MessagesListViewModel.RefreshResivedMessages(SWAM.MainWindow.LoggedInUser.Id);

        private void SendedMessages_Click(object sender, RoutedEventArgs e) => MessagesListViewModel.RefreshSendedMessages(SWAM.MainWindow.LoggedInUser.Id);
    }
}
