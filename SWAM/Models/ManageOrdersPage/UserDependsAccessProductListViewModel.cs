using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;

namespace SWAM.Models.ManageOrdersPage
{
    public class UserDependsAccessProductListViewModel : UserControl
    {
        private readonly ObservableCollection<State> _states = new ObservableCollection<State>();
        public ObservableCollection<State> States => this._states;

        private ObservableCollection<State> _selectedState = new ObservableCollection<State>();
        public ObservableCollection<State> SelectedState => this._selectedState;

        #region Singletone Pattern
        static UserDependsAccessProductListViewModel() => _instance.Refresh();

        private static readonly UserDependsAccessProductListViewModel _instance = new UserDependsAccessProductListViewModel();
        public static UserDependsAccessProductListViewModel Instance => _instance;
        #endregion

        #region Refresh
        public void Refresh()
        {
            var accessUsersToWarehouses = new List<AccessUsersToWarehouses>(AccessUsersToWarehouses.GetUserAccesses(SWAM.MainWindow.LoggedInUser.Id));

            if (accessUsersToWarehouses != null)
            {
                if (_states.Count > 0)
                    _states.Clear();

                accessUsersToWarehouses.ForEach(a => { Product.GetProductsFromWarehouse(a.Warehouse.Id).ForEach(s => { _states.Add(s); }); });
            }
        }
        #endregion

        public void SetSelectedState(State state)
        {
            if (_selectedState.Count > 0)
                _selectedState.Clear();

            _selectedState.Add(state);
        }
    }
}