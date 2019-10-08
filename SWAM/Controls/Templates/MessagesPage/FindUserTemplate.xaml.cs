using SWAM.Models.User;
using SWAM.Models.Messages;
using SWAM.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SWAM.Controls.Templates.MessagesPage
{
    /// <summary>
    /// Logika interakcji dla klasy FindUserTemplate.xaml
    /// </summary>
    public partial class FindUserTemplate : UserControl
    {
        public FindUserTemplate() => InitializeComponent();
        

        #region Row_DoubleClick
        /// <summary>
        /// Action after double click row in user list.
        /// </summary>
        /// <param name="sender">Data grid row</param>
        /// <param name="e">Action double clicked</param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is User user)
                SelectedUsersListViewModel.Instance.AddUser(user);
            
        }
        #endregion
        #region UserName_TextChanged
        /// <summary>
        /// Action after type text in text box named find user name.
        /// </summary>
        /// <param name="sender">Text box</param>
        /// <param name="e">Entered character</param>
        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(MessagesUsersList.Instance);
            filter.Filter = user =>
            {
                User allUsersWhose = user as User;
                return allUsersWhose.Name.Contains(FindUserName.Text);
            };
        }
        #endregion
        #region Confirm_Click
        /// <summary>
        /// Action after user click confirm users name.
        /// </summary>
        /// <param name="sender">Confirm button</param>
        /// <param name="e">Action clicked</param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow parent)
            {
                parent.SendMessageReplay.SetResceiver();
                parent.ChangeContent(Enumerators.BookmarkInPage.SendMessageMessagesWindow);
            }
        }
        #endregion

        private void CancelSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow sendMessageWindow)
            {
                sendMessageWindow.RefreshContents();
                sendMessageWindow.Hide();
            }
        }
    }
}
