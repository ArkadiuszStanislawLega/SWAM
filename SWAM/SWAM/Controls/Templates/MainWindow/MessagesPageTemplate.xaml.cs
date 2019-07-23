﻿using SWAM.Controls.Templates.AdministratorPage;
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

namespace SWAM.Controls.Templates.MainWindow
{
    /// <summary>
    /// Logika interakcji dla klasy MessagesPageTemplate.xaml
    /// </summary>
    public partial class MessagesPageTemplate : BasicUserControl
    {
        private Message _currentMessage;
        public MessagesPageTemplate()
        {
            InitializeComponent();

            //TODO: Delete this when sending message is work.
            SWAM.MainWindow.LoggedInUser.Messages = new List<Message>()
            {
                new Message()
                {
                    Sender = new User(){Name = "Mietek" },
                    Receiver = new User(){Name = "Czesiek"},
                    TitleOfMessage = "Witaj w systemie",
                    ContentOfMessage = "Zostałeś poprawnie dodany do systemu. Przestrzegaj zasad i regulaminu systemu.",
                    PostDate = DateTime.Now,
                    IsReaded = true
                },
                new Message()
                {
                    Sender = new User(){Name = "Czesiek" },
                    Receiver = new User(){Name = "Mietek" },
                    TitleOfMessage = "Faktura za gwoździe",
                    ContentOfMessage = "Czy to ty zrobiłeś fakturę numer 8000 na gwoździe, jest w niej błąd.",
                    PostDate = DateTime.Now
                },
                new Message()
                {
                    Sender = new User(){Name = "Monika" },
                    Receiver = new User(){Name = "Mietek" },
                    TitleOfMessage = "Potrzebuję numeru faktury",
                    ContentOfMessage = "Potrzebna jest faktura za gwoździe które Jan Kowalski kupił w zeszłym tygodniu. Tam była ilość 10000 gwoździ.",
                    PostDate = DateTime.Now
                }
            };

            DataContext = SWAM.MainWindow.LoggedInUser;
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
                this.DateOfSend.Text = message.PostDate.ToString();
                this.TitleOfMessage.Text = message.TitleOfMessage;
            }
            else InformationToUser($"{ErrorMesages.MESSAGE_READ_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion

        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessageWindow sendMessageWindow = new SendMessageWindow();
            sendMessageWindow.Tag = SWAM.MainWindow.MessagesWindows.Count;
            SWAM.MainWindow.MessagesWindows.Add(sendMessageWindow);
            SWAM.MainWindow.MessagesWindows[(int)sendMessageWindow.Tag].Show();
        }

        private void ReplayMessage_Click(object sender, RoutedEventArgs e)
        {
            if (this._currentMessage != null)
            {
                SendMessageWindow sendMessageWindow = new SendMessageWindow();
                sendMessageWindow.Tag = SWAM.MainWindow.MessagesWindows.Count;
                sendMessageWindow.SetReplayMessage(this._currentMessage);
                SWAM.MainWindow.MessagesWindows.Add(sendMessageWindow);
                SWAM.MainWindow.MessagesWindows[(int)sendMessageWindow.Tag].Show();
            }
        }


    }
}
