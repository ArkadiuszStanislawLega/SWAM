﻿using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.Messages
{
    /// <summary>
    /// View model list of users to whom messages are to be sent in SendMessageWindow.
    /// </summary>
    public class SelectedUsersListViewModel : UserControl
    {
        private ObservableCollection<User.User> _usersListViewModel = new ObservableCollection<User.User>();
        public ObservableCollection<User.User> UsersList { get => this._usersListViewModel; }

        #region Singletone Pattern
        static SelectedUsersListViewModel() { }

        private static readonly SelectedUsersListViewModel _instance = new SelectedUsersListViewModel();
        public static SelectedUsersListViewModel Instance => _instance;
        #endregion
        #region AddUser
        /// <summary>
        /// Adding new user to list.
        /// </summary>
        /// <param name="user">The user who is being added.</param>
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
        #endregion
        #region RemoveUser
        /// <summary>
        /// Removes the user from the list.
        /// </summary>
        /// <param name="user">User to be removed from the list.</param>
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
        #endregion
    }
}
