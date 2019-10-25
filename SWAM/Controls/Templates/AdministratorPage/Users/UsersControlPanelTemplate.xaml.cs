using System.Windows;
using System.Windows.Controls;
using SWAM.Models.User;
using SWAM.Models.AdministratorPage;
using SWAM.Strings;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Threading.Tasks;

namespace SWAM.Controls.Templates.AdministratorPage
{
 
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy UsersControlPanel.xaml
    /// </summary>
    public partial class UsersControlPanelTemplate : BasicUserControl
    {
        #region Basic Constructor
        public UsersControlPanelTemplate()
        {
            InitializeComponent();



            ChangeContent(c);
        }
        #endregion
        #region UsersControlPanelTemplate_Loaded
        private void UsersControlPanelTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersList();
            //Make filtr in user list ascending.
            UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
        }
        #endregion

        CreateNewUserTemplate c = new CreateNewUserTemplate();

        #region RefreshUsersList
        /// <summary>
        /// Refreshing view model of users list.
        /// </summary>
        public void RefreshUsersList() => UsersListViewModel.Instance.Refresh();
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
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            ChangeContent(c);
        }
        #endregion
        #region ChangeContent
        /// <summary>
        /// Changing content for the new one in right section of this user control.
        /// </summary>
        /// <param name="newContent">Profile of user template or New user template.</param>
        private void ChangeContent(BasicUserControl newContent)
        {
            if (this.RightSection.Children.Count > 0 && this.RightSection.Children[0] != newContent)
            {
                c.CreateStory();
                this.RightSection.Children.RemoveAt(0);
                this.RightSection.Children.Add(newContent);
            }
            else if(this.RightSection.Children.Count == 0)
            {
                c.UnloadStory.Completed += (s, e) =>
                {
                    this.RightSection.Children.Remove(c);
                };
                this.RightSection.Children.Add(c);
            }
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
            ICollectionView filter = CollectionViewSource.GetDefaultView(UsersListViewModel.Instance);
            filter.Filter = user =>
            {
                User allUsersWhose = user as User;
                return this.FiltrByName.IsChecked == true ? allUsersWhose.Name.Contains(this.FindUser.Text) : allUsersWhose.Permissions.ToString().Contains(this.FindUser.Text);
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

        #region Item_Click
        /// <summary>
        /// Action after click item from user list.
        /// Showing profile of clicked user.
        /// </summary>
        /// <param name="sender">Item from the list of users in left section.</param>
        /// <param name="e">Clicked item</param>
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
                ChangeContent(CreateUserProfile((int)button.Tag));
            else InformationToUser(ErrorMesages.DURING_CHANGING_USER_PROFILE_ERROR);
        }
        #endregion
    }
}
