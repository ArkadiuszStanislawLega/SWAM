using SWAM.Models.Messages;
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

        private void DeleteUser_Click(object sender, RoutedEventArgs e) => SelectedUsersListViewModel.Instance.RemoveUser(new User() { Id = (int)this.Tag });
    }
}
