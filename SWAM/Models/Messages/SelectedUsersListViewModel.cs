using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.Messages
{
    public class SelectedUsersListViewModel : UserControl
    {
        private ObservableCollection<User.User> _usersListViewModel = new ObservableCollection<User.User>();

        public ObservableCollection<User.User> UsersList { get => this._usersListViewModel; }

        public void AddUser(User.User user)
        {
            bool isAlreadySuchUser = false;

            foreach (User.User u in UsersList)
            {
                if (u.Id == user.Id)
                {
                    isAlreadySuchUser = true;
                    break;
                }
            }

            if(!isAlreadySuchUser)
                UsersList.Add(user);
        }
        public void RemoveUser(User.User user)
        {
            int numberOfUsers = this.UsersList.Count;
            if (numberOfUsers > 0)
            {
                for (int i = 0; i < numberOfUsers; i++)
                {
                    if (this.UsersList[i].Id == user.Id)
                    {
                        UsersList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

    }
}
