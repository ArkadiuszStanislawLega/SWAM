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
        private readonly ObservableCollection<Product> products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products => products;

        public void Refresh()
        {
            if (products.Count > 0)
                products.Clear();

            var productsListFromDb = Product.AllProducts();
            if (productsListFromDb != null)
            {
                foreach (var item in productsListFromDb)
                {
                    products.Add(item);
                }
            }
        }
    }
}
