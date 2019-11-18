using SWAM.Templates.AdministratorPage;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.AdministratorPage.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseListItemTemplate.xaml
    /// </summary>
    public partial class WarehouseListItemTemplate : Button
    {
        public WarehouseListItemTemplate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this).ShowProfile(this);
        
    }
}
