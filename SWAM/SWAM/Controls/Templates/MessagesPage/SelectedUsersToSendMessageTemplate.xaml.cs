using SWAM.Models.Messages;
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
    /// Logika interakcji dla klasy SelectedUsersToSendMessageTemplate.xaml
    /// </summary>
    public partial class SelectedUsersToSendMessageTemplate : UserControl
    {
        public SelectedUsersListViewModel SelectedUsersListViewModel { get; set; } = new SelectedUsersListViewModel();
        public SelectedUsersToSendMessageTemplate()
        {
            InitializeComponent();

            DataContext = SelectedUsersListViewModel;
        }
    }
}
