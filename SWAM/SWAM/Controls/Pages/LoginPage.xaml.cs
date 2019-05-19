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
using SWAM.Enumerators;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy loginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        new PagesUserControls NAME_OF_PAGE = PagesUserControls.LoginPage;

        public LoginPage(MainWindow mainWindow)       
            : base(mainWindow)
        {
            InitializeComponent();
        }

        override protected void ChangePage_Click(object sender, RoutedEventArgs e) => this._mainWindow.ChangeContent(PagesUserControls.ManageItemsPage);
    }
}
