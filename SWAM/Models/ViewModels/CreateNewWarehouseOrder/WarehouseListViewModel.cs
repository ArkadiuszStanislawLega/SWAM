using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models.ViewModels.CreateNewWarehouseOrder
{
   public class WarehouseListViewModel
    {
        #region Properties
        /// <summary>
        /// Customers list view model, holds all customers from database.
        /// </summary>
        private readonly ObservableCollection<Warehouse.Warehouse> _warehouses = new ObservableCollection<Warehouse.Warehouse>();
        /// <summary>
        /// Customers list view model, holds all customers from database.
        /// </summary>
        public ObservableCollection<Warehouse.Warehouse> Warehouses => _warehouses;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static WarehouseListViewModel() => _instance.Refresh();

        private static readonly WarehouseListViewModel _instance = new WarehouseListViewModel();
        public static WarehouseListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var warehouses = context.Warehouses
                    .Include(w=>w.WarehouseAddress)
                    .ToList();

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
