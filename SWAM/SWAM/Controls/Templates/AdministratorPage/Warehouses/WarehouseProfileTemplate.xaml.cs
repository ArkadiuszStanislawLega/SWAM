using SWAM.Models;
using SWAM.Models.AdministratorPage;
using System.Windows;


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

            if (DataContext is Warehouse data)
            {
                foreach (Warehouse w in warehouses.WarehousesList)
                {
                    if (w.Id == data.Id)
                    {
                        DataContext = w;
                        break;
                    }
                }
            }
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
