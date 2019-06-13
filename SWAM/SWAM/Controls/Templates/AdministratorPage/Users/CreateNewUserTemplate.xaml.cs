using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM;
using System.Security;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewUserTemplate.xaml
    /// </summary>
    public partial class CreateNewUserTemplate : UserControl
    {
        public CreateNewUserTemplate()
        {
            InitializeComponent();
        }

        #region Comfirm_Click
        /// <summary>
        /// Action after click creat user button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comfirm_Click(object sender, RoutedEventArgs e)
        {
            //Label in main menu to put information about creating new user.
            var informationLabel = SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).InformationLabel;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = new User()
                {
                    Name = this.NewUserName.Text,
                    Password = this.UserPassword.Password,
                    DateOfCreate = DateTime.Now,
                    Permissions = (Enumerators.UserType)this.UserPermissions.SelectedValue,
                    StatusOfUserAccount = this.AccountStatus.IsChecked == true ? Enumerators.StatusOfUserAccount.Active : Enumerators.StatusOfUserAccount.Blocked,
                    DateOfExpiryOfTheAccount = this.AccoutnExpireCallendar.SelectedDate
                };

                if (user != null)
                {
                    context.Users.Add(user);
                    informationLabel.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    informationLabel.Content = "Nie udało się dodać użytkownika " + user.Name;
                    informationLabel.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();
                SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this).RefreshUsersList();

                RestartTextBoxes();
            }
        }
        #endregion

        #region RestartTextBoxes
        /// <summary>
        /// Reset textboxes and combobox after create new user.
        /// </summary>
        private void RestartTextBoxes()
        {
            this.NewUserName.Text = "";
            this.UserPassword.Password = "";
            this.ConfirmPassword.Password = "";
            this.UserPermissions.SelectedValue = 0;
        }
        #endregion
    }
}
