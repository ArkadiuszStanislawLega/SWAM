using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.Entity;

namespace SWAM.Models.Messages
{
    public class MessagesUsersList : UserControl
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
                    .ToList();
            };

            foreach (User u in dbUsers)
                this._usersListViewModel.Add(u);
        }
    }
}
