using System.Collections.ObjectModel;
using System.Linq;

namespace SWAM.Models.ViewModels.CreateNewWarehouseOrder
{
    public class ProductListViewModel
    {
        #region Properties
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products => _products;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static ProductListViewModel() => _instance.Refresh();

        private static readonly ProductListViewModel _instance = new ProductListViewModel();
        public static ProductListViewModel Instance => _instance;
        #endregion

        #region Refresh
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh()
        {
            if (_products != null && _products.Count > 0)
                _products.Clear();

            var products = context.Products.ToList();

            foreach (var product in products)
            {
                _products.Add(product);
            }
        }
        #endregion
    }
}
