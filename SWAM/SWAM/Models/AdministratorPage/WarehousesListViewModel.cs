using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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



        public void RemoveAll()
        {
            _warehousesListViewModel.Clear();
        }
    }
}
