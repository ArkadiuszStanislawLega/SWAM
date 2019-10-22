using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.ManageOrdersPage
{
    public class UserDependsAccessProductListViewModel : UserControl
    {
        private readonly ObservableCollection<State> _states = new ObservableCollection<State>();
        public ObservableCollection<State> States => this._states;
        /// <summary>
        /// A list with ID numbers of warehouses that have already been checked when adding products to the collection.
        /// </summary>
        private List<int> _warehousesIds; 

        #region Singletone Pattern
        static UserDependsAccessProductListViewModel() => _instance.Refresh();

        private static readonly UserDependsAccessProductListViewModel _instance = new UserDependsAccessProductListViewModel();
        public static UserDependsAccessProductListViewModel Instance => _instance;
        #endregion 

        public void Refresh()
        {
            //I am getting all logged in user's permissions from the database.
            var accessUsersToWarehouses = AccessUsersToWarehouses.GetUserAccesses(SWAM.MainWindow.LoggedInUser.Id);
            this._warehousesIds = new List<int>(); 

            if (accessUsersToWarehouses != null)
            {
                if (_states.Count > 0)
                    _states.Clear();

                //I check all warehouses to which the logged in user has rights in terms of products.
                foreach (var item in accessUsersToWarehouses)
                {
                    //If the warehouse has not yet been checked..
                    if (!IsWarehouseProductsAreChecked(item.Warehouse.Id))
                    {
                        //I am downloading warehouse products from the database...
                        var states = Product.GetProductsFromWarehouse(item.Warehouse.Id);

                        //Adds the Warehouse ID to the list of checked warehouses...
                        this._warehousesIds.Add(item.Warehouse.Id);

                        //I browse the list of products from the warehouse to find one that is not yet in the collection.
                        foreach (var state in states)
                        {
                            if (!IsProductInList(state.Product))
                            {
                                _states.Add(state);
                            }
                        }
                    }
                }
            }
        }

        #region IsWarehouseProductsAreChecked
        /// <summary>
        /// Checks whether products from the warehouse were added to the collection.
        /// </summary>
        /// <param name="warehouseId">Warehouse Id</param>
        /// <returns>True if warehouse was checked.</returns>
        private bool IsWarehouseProductsAreChecked(int warehouseId)
        {
            foreach (var item in this._warehousesIds)
            {
                if (item == warehouseId)
                    return true;
            }

            return false;
        }
        #endregion
        #region IsProductInList
        /// <summary>
        /// Checks if the product is in the collection.
        /// </summary>
        /// <param name="product">Checked product.</param>
        /// <returns>True if product is in the collection.</returns>
        private bool IsProductInList(Product product)
        {
            foreach (var item in _states)
            {
                if (product.CompareTo(item) == 1 )
                    return true;
            }

            return false;
        }
        #endregion
    }
}