using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.Warehouse
{
    public class WarehouseOrderListViewModel : UserControl
    {
        private readonly ObservableCollection<WarehouseOrder> _warehousesOrders = new ObservableCollection<WarehouseOrder>();
        public ObservableCollection<WarehouseOrder> WarehouseOrders => this._warehousesOrders;

        #region Singletone Pattern
        static WarehouseOrderListViewModel() => _instance.Refresh();

        private static readonly WarehouseOrderListViewModel _instance = new WarehouseOrderListViewModel();
        public static WarehouseOrderListViewModel Instance => _instance;
        #endregion

        public void Refresh()
        {
            if (WarehouseOrder.GetAllOrders() is IList<WarehouseOrder> list)
            {
                if (this._warehousesOrders.Count > 0)
                    this._warehousesOrders.Clear();

                foreach (var item in list)
                {
                    this._warehousesOrders.Add(item);
                }
            }
        }
    }

}
