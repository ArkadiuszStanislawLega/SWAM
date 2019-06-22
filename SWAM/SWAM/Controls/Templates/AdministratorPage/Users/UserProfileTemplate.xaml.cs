using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;


namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class UserProfileTemplate : UserControl
    {

        public UserProfileTemplate()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            if (DataContext is User user)
            {
                //TODO: Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this.DataContext = context.Users.Include(u => u.Accesess)
                                                     .Include(u => u.Emails)
                                                     .Include(u => u.Phones)
                                                     .FirstOrDefault(u => u.Id == user.Id);
                }
            }
        }

        #region DeletUser_Click
        /// <summary>
        /// Action after click button delete user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make a window asking if your realy want to delete this user.
            //TODO: Try - catch
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (DataContext is User user)
                {
                    var userDb = context.Users.FirstOrDefault(u => u.Id == user.Id);

                    if (userDb != null)
                    {
                        context.Users.Remove(userDb);
                        context.SaveChanges();
                    }

                    var parent = SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this);
                    parent.RefreshUsersList();
                    //Show profile of the first user in the list.
                    if (parent.UsersList.Items.Count > 0 && parent.UsersList.Items[0] is User firstUser)
                        parent.ShowProfile(new UsersListItemTemplate() { Tag = firstUser.Id, DataContext = firstUser });

                    SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                                InformationForUser($"Użytkownik {user.Name} został usunięty.");
                }
            }
        }
        #endregion

        #region BlockUser_Click
        /// <summary>
        /// Action after click button block user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make a window asking if you really want to block this user.
            //TODO: Try - catch
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                if (DataContext is User user)
                {
                    var userDb = context.Users.FirstOrDefault(u => u.Id == user.Id);

                    if (userDb != null)
                    {
                        if (userDb.StatusOfUserAccount == Enumerators.StatusOfUserAccount.Active) userDb.StatusOfUserAccount = Enumerators.StatusOfUserAccount.Blocked;
                        else userDb.StatusOfUserAccount = Enumerators.StatusOfUserAccount.Active;

                        context.SaveChanges();

                        this.DataContext = context.Users.Include(u => u.Accesess)
                                                        .Include(u => u.Emails)
                                                        .Include(u => u.Phones)
                                                        .FirstOrDefault(u => u.Id == user.Id);
                    }
                }
            }
        }
        #endregion
        #region NewCommand_Executed
        /// <summary>
        /// Action after clock confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                var userType = (Enumerators.UserType)this.EditPermissions.SelectedValue;

                if (userType != user.Permissions)
                {
                    //TODO: Try - catch
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions = userType;
                        context.SaveChanges();

                        Permissions.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.ToString();
                    }

                    SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                            InformationForUser($"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}.");
                }
            }
        }

        #endregion
        #region EditNameCommand_Executed
        /// <summary>
        /// Action after click confrim change user name button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditNameCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                //TODO: Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    //TODO: Validation of user name.
                    context.Users.FirstOrDefault(u => u.Id == user.Id).Name = this.EditName.Text;
                    context.SaveChanges();
                }

                RefreshData();

                SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                        InformationForUser($"Nazwa użytkownika {user.Name} została zmienione na {this.EditName.Text}.");
            }
        }
        #endregion 
        #region EditUserPermissionsCommand_Executed
        /// <summary>
        /// Action after clock confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUserPermissionsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                var userType = (Enumerators.UserType)this.EditPermissions.SelectedValue;

                if (userType != user.Permissions)
                {
                    //TODO: Try - catch
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions = userType;
                        context.SaveChanges();

                        Permissions.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.ToString();
                    }

                    SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                            InformationForUser($"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}.");
                }
            }
        }
        #endregion  
    }
}
