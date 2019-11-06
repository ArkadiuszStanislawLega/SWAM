using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models.ViewModels.CreateNewWarehouseOrder
{
    public class ProductOrderListViewModel
    {
        private readonly ObservableCollection<WarehouseOrderPosition> _warehouseOrderPositions = new ObservableCollection<WarehouseOrderPosition>();
        public ObservableCollection<WarehouseOrderPosition> WarehouseOrderPositions => this._warehouseOrderPositions;

        #region Singletone Pattern
        private static readonly ProductOrderListViewModel _instance = new ProductOrderListViewModel();
        public static ProductOrderListViewModel Instance => _instance;
        #endregion 

        public void Add(WarehouseOrderPosition warehouseOrderPosition)
        {
            if (!IsAddedAlready(warehouseOrderPosition.Product))
                _warehouseOrderPositions.Add(warehouseOrderPosition);
        }

        public bool IsAddedAlready(Product product)
        {
            foreach (var position in _warehouseOrderPositions)
            {
                if (position.Product.Id == product.Id)
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            if (_warehouseOrderPositions.Count > 0)
                _warehouseOrderPositions.Clear();
        }

        public void Delete(int Id)
        {
            for (int i = 0; i < _warehouseOrderPositions.Count; i++)
            {
                if (_warehouseOrderPositions.ElementAt(i).ProductId == Id)
                    WarehouseOrderPositions.RemoveAt(i);
            }
        }
    }
}
