using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Data.Entity;

namespace SWAM.Models.AdministratorPage
{
    public class WarehousesListViewModel : UserControl
    {
        private readonly ObservableCollection<Warehouse.Warehouse> _warehousesListViewModel = new ObservableCollection<Warehouse.Warehouse>();

        public ObservableCollection<Warehouse.Warehouse> WarehousesList { get => this._warehousesListViewModel; }

        #region Singletone Pattern
        static WarehousesListViewModel() => _instance.Refresh();

        private static readonly WarehousesListViewModel _instance = new WarehousesListViewModel();
        public static WarehousesListViewModel Instance => _instance;
        #endregion 

        public void AddWarehouse(Warehouse.Warehouse warehouse)
        {
            bool idIsInWarehousesListViewModel = false;
            if (this._warehousesListViewModel != null && this._warehousesListViewModel.Count > 0)
            {
                foreach (Warehouse.Warehouse w in this._warehousesListViewModel)
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

            IList<Warehouse.Warehouse> dbWarehouses;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                dbWarehouses = context.Warehouses
                    .Include(a => a.WarehouseAddress)
                    .Include(co => co.CustomerOrders)
                    .Include(u => u.Accesses)
                    .ToList();

                //TODO: Make this more pro!!
                foreach (Warehouse.Warehouse w in dbWarehouses)
                {
                    for (int i = 0; i < w.Accesses.Count; i++)
                    {
                        int id = w.Accesses[i].Id;
                        w.Accesses[i] = context.AccessUsersToWarehouses
                          .Include(au => au.User)
                          .Include(au => au.Administrator)
                          .Include(au => au.Warehouse)
                          .FirstOrDefault(au => au.Id == id);
                    }
                }
            };

            foreach (Warehouse.Warehouse u in dbWarehouses)
                this._warehousesListViewModel.Add(u);
        }

        public void RemoveAll()
        {
            _warehousesListViewModel.Clear();
        }
    }

}
