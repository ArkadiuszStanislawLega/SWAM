using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SWAM.Models.ProductListViewModel
{
    public class ProductListViewModel : UserControl
    {
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products => _products;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static ProductListViewModel() { _instance.Refresh(); }

        private static readonly ProductListViewModel _instance = new ProductListViewModel();
        public static ProductListViewModel Instance => _instance;
        public ProductListViewModel() { }
        #endregion


        public void Refresh()
        {
            if (_products.Count > 0)
                _products.Clear();

            var productsListFromDb = Product.AllProducts();

            if (productsListFromDb != null)
            {
                foreach (var item in productsListFromDb)
                {
                    _products.Add(item);
                }
            }
        }

		public Product LastProduct()
		{
			Refresh();
			return _products.Last<Product>();			
		}

	}
}
