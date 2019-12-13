using System.Collections.ObjectModel;
using System.Linq;

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
            if (_warehouses.Count > 0)
                _warehouses.Clear();

            var warehouses = new ApplicationDbContext().Warehouses.ToList();

            foreach (var item in warehouses)
            {
                _warehouses.Add(item);
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
