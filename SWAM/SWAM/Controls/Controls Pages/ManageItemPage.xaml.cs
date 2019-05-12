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
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : Page
    {
        #region Properties
        /// <summary>
        /// Name of the current page.
        /// </summary>
        new const PagesUserControls NAME_OF_PAGE = PagesUserControls.ManageItemsPage;
        #endregion

        #region BasicConstructor
        public ManageItemPage(MainWindow mainWindow) 
            :base(mainWindow)
        {
            InitializeComponent();
        }
        #endregion

        override protected void ChangePage_Click(object sender, RoutedEventArgs e) => this._mainWindow.ChangeContent(PagesUserControls.AdministratorPage);
    }
}
