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

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy UsersControlPanel.xaml
    /// </summary>
    public partial class UsersControlPanel : UserControl
    {

        string[] _dataGridValues =
        {
            "Lp.",
            "Nazwa",
            "Uprawnienia"
        };

        User[] _users =
        {
            new User("Mietek", "tajneHasło", UsersType.Administrator),
            new User("Himen", "tajneHasło", UsersType.Seller),
            new User("Zordon", "tajneHasło", UsersType.Warehouseman),
            new User("Bernadeta", "tajneHasło", UsersType.Administrator),
            new User("Hermenegilda", "tajneHasło", UsersType.Administrator),
        };

        public string[] DataGridValues { get => this._dataGridValues; set => this._dataGridValues = value; }
        public User[] Users { get => _users; set => _users = value; }

        public UsersControlPanel()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}
