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
    public partial class UserProfileTemplate : BasicUserControl
    {
        public UserProfileTemplate()
        {
            InitializeComponent();
        }

        #region RefreshData
        /// <summary>
        /// Getting data from database and refresh profile.
        /// </summary>
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
        #endregion  
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

                    InformationToUser($"Użytkownik {user.Name} został usunięty.");
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

                        InformationToUser($"Status konta użytkownika {user.Name} została zmieniony na {userDb.StatusOfUserAccount.ToString()}.");
                    }
                }
            }
        }
        #endregion
    }
}
