using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Events.NavigationButton;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy UsersControlPanel.xaml
    /// </summary>
    public partial class UsersControlPanelTemplate : UserControl
    {
        #region Properties
        /// <summary>
        /// List with all users in service.
        /// </summary>
        private List<User> _users;
        #endregion

        #region Basic Constructor
        public UsersControlPanelTemplate()
        {
            InitializeComponent();

            DataContext = this;

            AddUsersList();
        }
        #endregion  
        #region AddUsersList
        /// <summary>
        /// Making visible list of all users in service.
        /// </summary>
        private void AddUsersList()
        {
            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                _users = application.Users.ToList();
            };

            for (int i = 0; i < this._users.Count; i++)
            {
                var usersListItemTemplate = new UsersListItemTemplate();
                usersListItemTemplate.DataContext = this._users[i];
                usersListItemTemplate.Tag = i;
                usersListItemTemplate.Click += UsersListItemTemplate_MouseLeftButtonDown;

                this.UsersList.Children.Add(usersListItemTemplate);
            }
        }
        #endregion

        #region UsersListItemTemplate_MouseLeftButtonDown
        /// <summary>
        /// Action after click item from the list with users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersListItemTemplate_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var button = sender as UsersListItemTemplate;
        
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);
      
            this.RightSection.Children.Add(CreateUserProfile((int)button.Tag));

            for (int i = 0; i < this.UsersList.Children.Count; i++)
            {
                var findSelectedButton = this.UsersList.Children[i] as UsersListItemTemplate;
                if (findSelectedButton.Tag != button.Tag) findSelectedButton.IsSelected = false;
                else findSelectedButton.IsSelected = true;
            }
        }
        #endregion
        #region CreateUserProfile
        /// <summary>
        /// Made view of the user profile in right section.
        /// </summary>
        /// <param name="userIndexInUsersList">Index number of UsersListItemTemplate in the users list.</param>
        /// <return>Chosen user profile.</return>
        private UserProfileTemplate CreateUserProfile(int userIndexInUsersList)
        {
            var userProfileTemplate = new UserProfileTemplate();
            userProfileTemplate.DataContext = this._users[userIndexInUsersList];
            return userProfileTemplate;
        }
        #endregion
    }
}
