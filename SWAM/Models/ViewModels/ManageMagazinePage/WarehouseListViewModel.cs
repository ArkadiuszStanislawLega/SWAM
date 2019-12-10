using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models.ViewModels.ManageMagazinePage
{
    public class WarehouseListViewModel
    {
        #region Properties
        private readonly ObservableCollection<Warehouse.Warehouse> _warehouses = new ObservableCollection<Warehouse.Warehouse>();
        public ObservableCollection<Warehouse.Warehouse> Warehouses => _warehouses;
        #endregion

        #region SingletonePattern
        static WarehouseListViewModel() => _instance.Refresh();

        private static readonly WarehouseListViewModel _instance = new WarehouseListViewModel();
        public static WarehouseListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        public void Refresh()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var warehouses = context.Warehouses.ToList();

                if (warehouses != null && _warehouses.Count > 0)
                    _warehouses.Clear();

                foreach (var warehouse in warehouses)
                {
                    _warehouses.Add(warehouse);
                }
            }
        }
        #endregion
    }
}
