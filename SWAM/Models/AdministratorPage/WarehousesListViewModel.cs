using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Data.Entity;
using System;

namespace SWAM.Models.AdministratorPage
{
    public class WarehousesListViewModel : UserControl
    {
        private ObservableCollection<Warehouse.Warehouse> _warehousesListViewModel = new ObservableCollection<Warehouse.Warehouse>();

        public ObservableCollection<Warehouse.Warehouse> WarehousesList { get => this._warehousesListViewModel; }

        public int Size => this._warehousesListViewModel.Count;

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
            //this._warehousesListViewModel.Clear();

            //IList<Warehouse> dbWarehouses;
            //using (ApplicationDbContext application = new ApplicationDbContext())
            //{
            //    dbWarehouses = application.Warehouses
            //        .Include(a => a.Address)
            //        .Include(co => co.CustomerOrders)
            //        .Include(u => u.Accesses)
            //        .ToList();

            //    //TODO: Make this more pro!!
            //    foreach(Warehouse w  in dbWarehouses)
            //    {
            //        foreach(AccessUsersToWarehouses a in w.Accesses)
            //        {
            //            a.User = User.GetUser(a.UserId);
            //            a.Administrator = User.GetUser(a.AdministratorId);
            //        }
            //    }
            //};

            //foreach (Warehouse u in dbWarehouses)
            //    this._warehousesListViewModel.Add(u);
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            _warehousesListViewModel.Clear();
        }
    }
}
