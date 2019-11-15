using System.Collections.ObjectModel;
using System.Windows.Controls;

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

        #region Refresh
        /// <summary>
        /// Gets the list of warehouses from the database.
        /// </summary>
        public void Refresh()
        {
            if(this._warehousesListViewModel.Count > 0)
                this._warehousesListViewModel.Clear();

            foreach (Warehouse.Warehouse u in Warehouse.Warehouse.GetAllWarehousesIncludingFullAccesess())
                this._warehousesListViewModel.Add(u);
        }
        #endregion
    }

}
