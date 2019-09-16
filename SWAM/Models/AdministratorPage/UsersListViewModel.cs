using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.Entity;

namespace SWAM.Models.AdministratorPage
{
    /// <summary>
    /// View model with list of Users in Administrator page.
    /// </summary>
    public class UsersListViewModel : UserControl
    {
        private ObservableCollection<User> _usersListViewModel = new ObservableCollection<User>();

        public ObservableCollection<User> UsersList { get => this._usersListViewModel; }

        public void Refresh()
        {
            this._usersListViewModel.Clear();

            IList<User> dbUsers;
            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                dbUsers = application.Users
                    .Include(u => u.Phones)
                    .Include(u => u.Emails)
                    .Include(u => u.Accesess)
                    .ToList();
            };

            foreach (User u in dbUsers)
                this._usersListViewModel.Add(u);
        }

        public void RemoveAll()
        {
            _usersListViewModel.Clear();
        }
    }
}
