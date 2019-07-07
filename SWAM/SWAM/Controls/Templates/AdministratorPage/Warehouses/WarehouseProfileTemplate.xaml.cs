using SWAM.Models;
using SWAM.Models.AdministratorPage;
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

namespace SWAM.Controls.Templates.AdministratorPage.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseProfileTemplate.xaml
    /// </summary>
    public partial class WarehouseProfileTemplate : BasicUserControl
    {
        public WarehouseProfileTemplate()
        {
            InitializeComponent();
            Loaded += WarehouseProfileTemplate_Loaded;
        }

        private void WarehouseProfileTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            WarehousesListViewModel warehouses = new WarehousesListViewModel();
            warehouses.Refresh();

            var data = DataContext as Warehouse;
            foreach (Warehouse w in warehouses.WarehousesList)
                if (w.Id == data.Id)
                    DataContext = w;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Address.ShowEditControls();
            this.WarehouseTechnicalDate.ShowEditControls();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Address.HideEditControls();
            this.WarehouseTechnicalDate.HideEditControls();
        }
    }
}
