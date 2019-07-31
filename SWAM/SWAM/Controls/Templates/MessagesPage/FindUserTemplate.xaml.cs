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
        public static MessagesUsersList MessagesUsersList { get; set; } = new MessagesUsersList();
        private User _selectedUser;

        public FindUserTemplate()
        {
            InitializeComponent();

            DataContext = MessagesUsersList;
            MessagesUsersList.Refresh();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is User user)
            {
                this.UserName.Text = user.Name;
                this._selectedUser = user;
            }
        }
        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(MessagesUsersList.UsersList);
            filter.Filter = user =>
            {
                User allUsersWhose = user as User;
                return allUsersWhose.Name.Contains(UserName.Text);
            };
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SendMessageWindow>(this) is SendMessageWindow parent)
            {
                parent.SendMessageReplay.SetResceiver(this._selectedUser);
                parent.ChangeContent(Enumerators.BookmarkInPage.SendMessageMessagesWindow);
            }
        }
    }
}
