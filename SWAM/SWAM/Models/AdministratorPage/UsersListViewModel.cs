using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SWAM.Models.AdministratorPage
{
    /// <summary>
    /// View model with list of Users in Administrator page.
    /// </summary>
    public class UsersListViewModel : UserControl
    {
        private ObservableCollection<User> _usersListViewModel = new ObservableCollection<User>();

        public ObservableCollection<User> UsersList { get => this._usersListViewModel; }

        public void AddUser(User user)
        {
            bool idIsInUsersListViewModel = false;
            if (this._usersListViewModel != null && this._usersListViewModel.Count > 0)
            {
                foreach (User u in this._usersListViewModel)
                {
                    if (user.Id == u.Id)
                    {
                        idIsInUsersListViewModel = true;
                        break;
                    }
                }
                if (!idIsInUsersListViewModel) this._usersListViewModel.Add(user);
            }
            else this._usersListViewModel.Add(user);
        }
    }
}
