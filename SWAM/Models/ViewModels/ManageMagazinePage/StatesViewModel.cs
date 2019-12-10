using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models.ViewModels.ManageMagazinePage
{
    public class StatesViewModel
    {
        private readonly ObservableCollection<Warehouse.Warehouse> _warehouses = new ObservableCollection<Warehouse.Warehouse>();
        public ObservableCollection<Warehouse.Warehouse> Warehouses => this._warehouses;

        private readonly ObservableCollection<State> _states = new ObservableCollection<State>();
        public ObservableCollection<State> States => this._states;

        #region Singletone Pattern
        static StatesViewModel() => _instance.Refresh();

        private static readonly StatesViewModel _instance = new StatesViewModel();
        public static StatesViewModel Instance => _instance;
        #endregion

        #region Refresh
        public void Refresh()
        {
            var accessUsersToWarehouses = (AccessUsersToWarehouses.GetUserAccesses(SWAM.MainWindow.LoggedInUser.Id)) as List<AccessUsersToWarehouses>;

            if (accessUsersToWarehouses != null)
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
