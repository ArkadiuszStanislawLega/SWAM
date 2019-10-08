using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.ManageOrdersPage
{
    public class UserDependsAccessProductListViewModel : UserControl
    {
        private readonly ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products => this._products;

        #region Singletone Pattern
        static UserDependsAccessProductListViewModel() => _instance.Refresh();

        private static readonly UserDependsAccessProductListViewModel _instance = new UserDependsAccessProductListViewModel();
        public static UserDependsAccessProductListViewModel Instance => _instance;
        #endregion 

        public void Refresh()
        {
            var accessUsersToWarehouses = AccessUsersToWarehouses.GetUserAccess(SWAM.MainWindow.LoggedInUser.Id);
            if (accessUsersToWarehouses != null)
            {
                if (_products.Count > 0)
                    _products.Clear();

                var list = Product.GetProductsFromWarehouse(accessUsersToWarehouses.Warehouse.Id);
                foreach (var item in list)
                {
                    _products.Add(item);
                }
            }
        }
    }
}
