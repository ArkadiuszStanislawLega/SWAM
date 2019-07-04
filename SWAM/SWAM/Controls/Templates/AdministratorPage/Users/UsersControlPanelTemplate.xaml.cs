﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM.Models;
using SWAM.Models.AdministratorPage;
using System.Data.Entity;

namespace SWAM.Controls.Templates.AdministratorPage
{
    using System;
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

            UsersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
        }

        private void UsersControlPanelTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersList();

            UsersList.Height = RightSection.Height - FindUserOrCreate.Height;

            DataContext = UserListViewModel;
        }
        #endregion

        #region UsersControlPanelTemplate_SizeChanged
        private void UsersControlPanelTemplate_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //TODO: Veryfy this 64.
            if (SWAM.MainWindow.IsMaximized) this.Height = SystemParameters.PrimaryScreenHeight - EverythingExceptTheMainContentHeight - 64;
            else this.Height = HeightOfAppliaction - EverythingExceptTheMainContentHeight - 64;

            ChangeSizeOfScrollInProfile();
        }
        #endregion

        #region ChangeSizeOfScrollInProfile
        /// <summary>
        /// Changing size of scrollViewer in profile with list to accesses to warehouses. 
        /// </summary>
        private void ChangeSizeOfScrollInProfile()
        {
            if (RightSection.Children != null
               && RightSection.Children.Count > 0
               && RightSection.Children[0] != null
               && RightSection.Children[0] is UserProfileTemplate profile) profile.AccesToWarehousesList.FitViewToFillInParent();
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
                return allUsersWhose.Name.Contains(FindUser.Text);
            };
        }
        #endregion
    }
}
