using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.ManageOrdersPage
{
    public class UserDependsAccessProductListViewModel : UserControl
    {
        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();

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
                Products = new ObservableCollection<Product>(Product.GetProductsFromWarehouse(accessUsersToWarehouses.Warehouse.Id));
            }
        }
    }
}
