using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Data.Entity;

namespace SWAM.Models.AdministratorPage
{
    public class WarehousesListViewModel : UserControl
    {
        private ObservableCollection<Warehouse> _warehousesListViewModel = new ObservableCollection<Warehouse>();

        public ObservableCollection<Warehouse> WarehousesList { get => this._warehousesListViewModel; }

        public int Size => this._warehousesListViewModel.Count;

        public void AddWarehouse(Warehouse warehouse)
        {
            bool idIsInWarehousesListViewModel = false;
            if (this._warehousesListViewModel != null && this._warehousesListViewModel.Count > 0)
            {
                foreach (Warehouse w in this._warehousesListViewModel)
                {
                    if (warehouse.Id == w.Id)
                    {
                        idIsInWarehousesListViewModel = true;
                        break;
                    }
                }
                if (!idIsInWarehousesListViewModel) this._warehousesListViewModel.Add(warehouse);
            }
            else this._warehousesListViewModel.Add(warehouse);
        }

        public void Refresh()
        {
            this._warehousesListViewModel.Clear();

            IList<Warehouse> dbWarehouses;
            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                dbWarehouses = application.Warehouses
                    .Include(a => a.Address)
                    .Include(co => co.CustomerOrders)
                    .Include(u => u.Users)
                    .ToList();
            };

            foreach (Warehouse u in dbWarehouses)
                this._warehousesListViewModel.Add(u);
        }

        public void RemoveAll()
        {
            _warehousesListViewModel.Clear();
        }
    }
}
