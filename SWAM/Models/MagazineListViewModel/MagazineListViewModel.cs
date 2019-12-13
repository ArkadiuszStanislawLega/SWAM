using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.MagazineListViewModel
{
    public class MagazineListViewModel : UserControl
    {
        private readonly ObservableCollection<State> states = new ObservableCollection<State>();
        public ObservableCollection<State> States => states;

        public void Refresh()
        {
            if (states.Count > 0)
                states.Clear();

            var statesListFromDb = State.AllStates();
            if (statesListFromDb != null)
            {
                foreach (var item in statesListFromDb)
                {
                    states.Add(item);
                }
            }
        }
    }
}
