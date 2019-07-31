using SWAM.Models;
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
        #region Properties
        /// <summary>
        /// List with all users in database.
        /// </summary>
        public static MessagesUsersList MessagesUsersList { get; set; } = new MessagesUsersList();
        /// <summary>
        /// Selected user.
        /// </summary>
        private User _selectedUser;
        #endregion

        public FindUserTemplate()
        {
            InitializeComponent();

            DataContext = MessagesUsersList;
            MessagesUsersList.Refresh();
        }

        #region Row_DoubleClick
        /// <summary>
        /// Action after double click row in user list.
        /// </summary>
        /// <param name="sender">Data grid row</param>
        /// <param name="e">Action double clicked</param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is User user)
            {
                this.FindUserName.Text = user.Name;
                this._selectedUser = user;
            }
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
            ICollectionView filter = CollectionViewSource.GetDefaultView(MessagesUsersList.UsersList);
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
                parent.SendMessageReplay.SetResceiver(this._selectedUser);
                parent.ChangeContent(Enumerators.BookmarkInPage.SendMessageMessagesWindow);
            }
        }
        #endregion
    }
}
