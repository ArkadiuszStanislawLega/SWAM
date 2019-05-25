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
using System.Collections.ObjectModel;
using SWAM.Models.AdministratorPage;

namespace SWAM.Controls.Templates.AdministratorPage
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
        /// <summary>
        /// View model of item in users list.
        /// </summary>
        public static UsersListViewModel UserListViewModel { get; set; }
        #endregion

        #region Basic Constructor
        public UsersControlPanelTemplate()
        {
            DataContext = UserListViewModel;

            InitializeComponent();

            UserListViewModel = new UsersListViewModel();
            SizeChanged += UsersControlPanelTemplate_SizeChanged;

        }

        private void UsersControlPanelTemplate_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SWAM.MainWindow.IsMaximized)
                this.UsersList.MaxHeight = SWAM.MainWindow.CurrentMonitorDeviceHigh - 185;
            else
                this.UsersList.MaxHeight = SWAM.MainWindow.HeightOfAppliaction - 185;
        }
        #endregion
        #region Overrided Methods
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            RefreshUsersList();
    
            UsersList.Height = RightSection.Height - FindUserOrCreate.Height;

            DataContext = UserListViewModel;
        }
        #endregion
        #region RefreshUsersList
        /// <summary>
        /// Refreshing view model of users list.
        /// </summary>
        public void RefreshUsersList()
        {
            if (this._users != null && this._users.Count > 0) this._users.Clear();

            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                _users = application.Users.ToList();
            };

            foreach (User u in _users)
                UserListViewModel.AddUser(u);

        }
        #endregion
        #region ShowPrfile
        /// <summary>
        /// Showing the profile of user after
        /// after click item from the list with users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowProfile(UsersListItemTemplate usersListItemTemplate)
        {
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);

            this.RightSection.Children.Add(CreateUserProfile((int)usersListItemTemplate.Tag));
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
            User currentUser = new User();

            foreach(User u in _users)
                if (u.Id == userIndexInUsersList)
                    currentUser = u;

            userProfileTemplate.DataContext = currentUser;
            return userProfileTemplate;
        }
        #endregion
        #region AddNewUser_Click
        /// <summary>
        /// Creating in right section user control for create new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);

            this.RightSection.Children.Add(new CreateNewUserTemplate());
        }
        #endregion
    }
}
