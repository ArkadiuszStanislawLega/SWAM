using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.AdministratorPage
{
    /// <summary>
    /// View model with list of Users in Administrator page.
    /// </summary>
    public class UsersListViewModel : UserControl
    {
        private readonly static ObservableCollection<User.User> _usersList = new ObservableCollection<User.User>();
        public static ObservableCollection<User.User> UsersList => _usersList;

        #region Singletone Pattern
        static UsersListViewModel() => _instance.Refresh();

        private static readonly UsersListViewModel _instance = new UsersListViewModel();
        public static UsersListViewModel Instance => _instance;
        public UsersListViewModel() => Refresh();
        #endregion
        #region Refresh
        /// <summary>
        /// Refreshes the list of users and retrieves them from the database.
        /// </summary>
        public void Refresh()
        {
            if(_usersList.Count > 0)
                _usersList.Clear();

            var dbUsers = User.User.AllUsersList();

            if (dbUsers != null)
            {
                foreach (User.User u in dbUsers)
                    _usersList.Add(u);
            }
        }
        #endregion
    }
}
