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
           if( SWAM.MainWindow.FindParent<SelectedUsersToSendMessageTemplate>(this) is SelectedUsersToSendMessageTemplate parent)
                parent.SelectedUsersListViewModel.RemoveUser(new Models.User() { Id = (int)this.Tag });
        }
    }
}
