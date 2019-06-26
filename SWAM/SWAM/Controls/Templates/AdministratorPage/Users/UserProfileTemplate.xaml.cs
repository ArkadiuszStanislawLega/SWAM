using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using SWAM.Exceptions;

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
            try
            {
                if (DataContext is User user)
                {
                    //TODO: Try - catch
                    using (var context = new ApplicationDbContext())
                    {
                        this.DataContext = context.Users.Include(u => u.Accesess)
                                                         .Include(u => u.Emails)
                                                         .Include(u => u.Phones)
                                                         .FirstOrDefault(u => u.Id == user.Id);
                    }
                    if(DataContext == null) throw new RefreshUserProfileException();
                }
                else throw new RefreshUserProfileException("RefreshData");
            }
            catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
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
            if (DataContext is User user)
            {
                user.Remove();
                InformationToUser($"Użytkownik {user.Name} został usunięty.");

                try
                {
                    var parent = SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this);
                    if (parent != null)
                    {
                        parent.RefreshUsersList();

                        //Show profile of the first user in the list.
                        if (parent.UsersList.Items.Count > 0 && parent.UsersList.Items[0] is User firstUser)
                            parent.ShowProfile(new UsersListItemTemplate() { Tag = firstUser.Id, DataContext = firstUser });
                    }
                    else throw new RefreshUserProfileException(ErrorMesages.DURING_DELETE_USER_ERROR);
                }
                catch (RefreshUserProfileException ex) { ex.ShowMessage(this); }
            }
            else InformationToUser(ErrorMesages.DURING_DELETE_USER_ERROR, true);
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
            if (DataContext is User user)
            {
                //its needed to clear the datacontext because the refresh function did not work properly after downloading the data after the change
                this.DataContext = null;
                if (user.StatusOfUserAccount == Enumerators.StatusOfUserAccount.Active) user.ChangeStatus(Enumerators.StatusOfUserAccount.Blocked);
                else user.ChangeStatus(Enumerators.StatusOfUserAccount.Active);
                InformationToUser($"Status konta użytkownika {user.Name} została zmieniony.");

                this.DataContext = User.GetUser(user.Id);
            }
            else InformationToUser(ErrorMesages.DURING_CHANGING_STATUS_USER_ACCOUT_ERROR, true);
        }
        #endregion
    }
}
