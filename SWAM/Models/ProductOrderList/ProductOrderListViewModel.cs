using SWAM.Models.Customer;
using System.Collections.ObjectModel;
using System.Linq;

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
            if (!IsAddedAlready(customerOrderPosition.Product))
                _customerOrderPositions.Add(customerOrderPosition);
        }

        public bool IsAddedAlready(Product product)
        {
            foreach (var position in _customerOrderPositions)
            {
                if (position.Product.Id == product.Id)
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            if (_customerOrderPositions.Count > 0)
                _customerOrderPositions.Clear();
        }

        public void Delete(int Id)
        {
            for (int i = 0; i < _customerOrderPositions.Count; i++)
            {
                if (_customerOrderPositions.ElementAt(i).ProductId == Id)
                    CustomerOrderPositions.RemoveAt(i);
            }
        }
    }
}