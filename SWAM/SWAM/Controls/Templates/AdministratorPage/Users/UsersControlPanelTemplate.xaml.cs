using System.Windows;
using System.Windows.Controls;
using SWAM.Models;
using SWAM.Models.AdministratorPage;

namespace SWAM.Controls.Templates.AdministratorPage
{
    using System.ComponentModel;
    using System.Windows.Data;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy UsersControlPanel.xaml
    /// </summary>
    public partial class UsersControlPanelTemplate : BasicUserControl
    {
        #region Properties
        /// <summary>
        /// View model of item in users list.
        /// </summary>
        public static UsersListViewModel UserListViewModel { get; set; } = new UsersListViewModel();
        #endregion

        #region Basic Constructor
        public UsersControlPanelTemplate()
        {
            InitializeComponent();
        }
        #endregion
        #region UsersControlPanelTemplate_Loaded
        private void UsersControlPanelTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersList();
            //Make filtr in user list ascending.
            UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            //Take userlist from database.
            DataContext = UserListViewModel;
        }
        #endregion

        #region RefreshUsersList
        /// <summary>
        /// Refreshing view model of users list.
        /// </summary>
        public void RefreshUsersList() => UserListViewModel.Refresh();
        #endregion

        #region ShowProfile
        /// <summary>
        /// Showing the profile of user,
        /// after click item from the list with users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowProfile(UsersListItemTemplate usersListItemTemplate) => ChangeContent(CreateUserProfile((int)usersListItemTemplate.Tag));
        #endregion
        #region CreateUserProfile
        /// <summary>
        /// Made view of the user profile in right section.
        /// </summary>
        /// <param name="userIndexInUsersList">Index number of UsersListItemTemplate in the users list.</param>
        /// <return>Chosen user profile.</return>
        private UserProfileTemplate CreateUserProfile(int userIndexInUsersList) => new UserProfileTemplate() { DataContext = User.GetUser(userIndexInUsersList) };
        #endregion
        #region AddNewUser_Click
        /// <summary>
        /// Creating in right section user control for create new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewUser_Click(object sender, RoutedEventArgs e) => ChangeContent(new CreateNewUserTemplate());
        #endregion
        #region ChangeContent
        /// <summary>
        /// Changing content for the new one in right section of this user control.
        /// </summary>
        /// <param name="newContent">Profile of user template or New user template.</param>
        private void ChangeContent(UserControl newContent)
        {
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);

            this.RightSection.Children.Add(newContent);
        }
        #endregion

        #region TextBox_TextChanged
        /// <summary>
        /// Filtering list depends on text typed in TextBox named FindUser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(UserListViewModel.UsersList);
            filter.Filter = user =>
            {
                User allUsersWhose = user as User;
                return this.FiltrByName.IsChecked == true ? allUsersWhose.Name.Contains(FindUser.Text) : allUsersWhose.Permissions.ToString().Contains(FindUser.Text);
            };
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after click checkBox in filters container to change type of sorting(ascending/descending) user list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (UsersList.Items.SortDescriptions.Count > 0)
                UsersList.Items.SortDescriptions.RemoveAt(UsersList.Items.SortDescriptions.Count-1);

            if (SortAscending.IsChecked == true)
                UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            else
                UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion
    }
}
