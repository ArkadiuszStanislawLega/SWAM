using System.Collections.ObjectModel;


namespace SWAM.Models.ProductOrderList
{
    public class ProductOrderListViewModel
    {
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products => this._products;

        #region Singletone Pattern

        private static readonly ProductOrderListViewModel _instance = new ProductOrderListViewModel();
        public static ProductOrderListViewModel Instance => _instance;
        #endregion 

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Clear()
        {
            if (_products.Count > 0)
                _products.Clear();
        }
    }
}
