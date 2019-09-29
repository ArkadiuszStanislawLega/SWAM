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
        private ObservableCollection<User.User> _usersListViewModel = new ObservableCollection<User.User>();

        public ObservableCollection<User.User> UsersList { get => this._usersListViewModel; }

        public void Refresh()
        {
            this._usersListViewModel.Clear();

            IList<User.User> dbUsers;
            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                throw new NotImplementedException();
                //dbUsers = application.Users
                //    .Include(u => u.Phones)
                //    .ToList();
            };

            foreach (User.User u in dbUsers)
                this._usersListViewModel.Add(u);
        }
    }
}
