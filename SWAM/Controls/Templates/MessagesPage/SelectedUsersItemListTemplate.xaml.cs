using SWAM.Models.User;
using System.Windows;
using System.Windows.Controls;


namespace SWAM.Controls.Templates.MessagesPage
{
    /// <summary>
    /// Logika interakcji dla klasy SelectedUsersItemListTemplate.xaml
    /// </summary>
    public partial class SelectedUsersItemListTemplate : UserControl
    {
        public SelectedUsersItemListTemplate()
        {
            InitializeComponent();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<SelectedUsersToSendMessageTemplate>(this) is SelectedUsersToSendMessageTemplate parent)
                parent.SelectedUsersListViewModel.RemoveUser(new User() { Id = (int)this.Tag });

            else if (SWAM.MainWindow.FindParent<SendMessageTemplate>(this) is SendMessageTemplate sendMessageTemplate)
                sendMessageTemplate.SelectedUsersListViewModel.RemoveUser(new User() { Id = (int)this.Tag });
        }
    }
}
