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
using System.Windows.Shapes;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        new Pages NAME_OF_PAGE = Pages.administratorPage;

        public AdministratorPage(MainWindow mainWindow) 
            : base(mainWindow)
        {
            InitializeComponent();
        }

        override protected void ChangePage_Click(object sender, RoutedEventArgs e) => this._mainWindow.ChangeContent(Pages.loginPage);
    }
}
