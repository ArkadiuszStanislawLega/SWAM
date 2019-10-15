using SWAM.Models.Customer;
using System.Collections.ObjectModel;

namespace SWAM.Models.ProductOrderList
{
    public class ProductOrderListViewModel
    {
        private readonly ObservableCollection<CustomerOrderPosition> _customerOrderPositions = new ObservableCollection<CustomerOrderPosition>();
        public ObservableCollection<CustomerOrderPosition> CustomerOrderPositions => this._customerOrderPositions;

        #region Singletone Pattern

        private static readonly ProductOrderListViewModel _instance = new ProductOrderListViewModel();
        public static ProductOrderListViewModel Instance => _instance;
        #endregion 

        public void Add(CustomerOrderPosition customerOrderPosition)
        {
            _customerOrderPositions.Add(customerOrderPosition);
        }

        public void Clear()
        {
            if (_customerOrderPositions.Count > 0)
                _customerOrderPositions.Clear();
        }
    }
}