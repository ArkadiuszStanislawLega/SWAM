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
    /// Logika interakcji dla klasy ManageOrdersPage.xaml
    /// </summary>
    public partial class ManageOrdersPage : Page
    {
        new const PagesUserControls NAME_OF_PAGE = PagesUserControls.ManageOrdersPage;

        public ManageOrdersPage(MainWindow mainWindow)
            :base(mainWindow)
        {
            InitializeComponent();
        }

        protected override void ChangePage_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
