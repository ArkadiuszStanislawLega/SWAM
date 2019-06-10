using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM.Models;
using SWAM.Models.AdministratorPage;
using System.Data.Entity;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy UsersControlPanel.xaml
    /// </summary>
    public partial class UsersControlPanelTemplate : UserControl
    {
        #region Properties
        /// <summary>
        /// View model of item in users list.
        /// </summary>
        public static UsersListViewModel UserListViewModel { get; set; }
        #endregion

        #region Basic Constructor
        public UsersControlPanelTemplate()
        {
            DataContext = UserListViewModel;

            UserListViewModel = new UsersListViewModel();
            UserListViewModel.Refresh();

            InitializeComponent();

        }
        #endregion

        #region UsersControlPanelTemplate_SizeChanged
        private void UsersControlPanelTemplate_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //TODO: make some static values for this below "185"
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

            UserListViewModel.Refresh();


        }
        #endregion
        #region ShowProfile
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


            //Items in list are tagget in userListItemTemplate. Tag = UserId.
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
            //finding user whith specific id from user list
            var context = new ApplicationDbContext();
            var user = context.Users.Include(u => u.Accesess)
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefault(u => u.Id == userIndexInUsersList);

           return new UserProfileTemplate() { DataContext = user };

      
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
