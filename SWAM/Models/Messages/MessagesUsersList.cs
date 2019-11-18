using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Data.Entity;

namespace SWAM.Models.Messages
{
    public class MessagesUsersList : UserControl
    {
        private readonly ObservableCollection<User.User> _usersListViewModel = new ObservableCollection<User.User>();

        public ObservableCollection<User.User> UsersList => _usersListViewModel; 

        #region Singletone Pattern
        static MessagesUsersList() => _instance.Refresh();

        private static readonly MessagesUsersList _instance = new MessagesUsersList();
        public static MessagesUsersList Instance => _instance;
        #endregion
        #region Refresh
        /// <summary>
        /// Gets the list of users from the database.
        /// </summary>
        public void Refresh()
        {
            _usersListViewModel.Clear();

            IList<User.User> dbUsers;
            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                dbUsers = application.People.OfType<User.User>()
                    .Include(u => u.Phones)
                    .ToList();
            };

            foreach (User.User u in dbUsers)
                _usersListViewModel.Add(u);
        }
        #endregion
    }
}
