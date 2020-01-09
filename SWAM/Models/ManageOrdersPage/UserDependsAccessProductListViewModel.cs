using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;
using SWAM.Models.ProductOrderList;

namespace SWAM.Models.ManageOrdersPage
{
    public class UserDependsAccessProductListViewModel : UserControl
    {
        private readonly ObservableCollection<Warehouse.Warehouse> _warehouses = new ObservableCollection<Warehouse.Warehouse>();
        public ObservableCollection<Warehouse.Warehouse> Warehouses => this._warehouses;

        private readonly ObservableCollection<State> _states = new ObservableCollection<State>();
        public ObservableCollection<State> States => this._states;

        #region Singletone Pattern
        static UserDependsAccessProductListViewModel() => _instance.Refresh();

        private static readonly UserDependsAccessProductListViewModel _instance = new UserDependsAccessProductListViewModel();
        public static UserDependsAccessProductListViewModel Instance => _instance;
        #endregion

        #region Refresh
        public void Refresh()
        {
            if (MainWindow.LoggedInUser != null)
            {
                if (AccessUsersToWarehouses.GetUserAccesses(MainWindow.LoggedInUser.Id) is List<AccessUsersToWarehouses> accessUsersToWarehouses)
                {
                    if (_warehouses.Count > 0)
                        _warehouses.Clear();

                    foreach (var item in accessUsersToWarehouses)
                    {
                        _warehouses.Add(item.Warehouse);
                    }
                }

                StatesRefresh();
            }
        }
        #endregion

        #region SetStates
        public void SetStates(Warehouse.Warehouse warehouse)
        {
            if (_states.Count > 0)
                _states.Clear();

            var statesFromWarehouse = State.GetStatesFromWarehouse(warehouse.Id);

            foreach (var item in statesFromWarehouse)
            {
                _states.Add(item);
            }
        }
        #endregion

        #region StatesRefresh
        public void StatesRefresh()
        {
            if (_states.Count > 0)
                SetStates(_states.ElementAtOrDefault(0).Warehouse);
        }
        #endregion
    }
}