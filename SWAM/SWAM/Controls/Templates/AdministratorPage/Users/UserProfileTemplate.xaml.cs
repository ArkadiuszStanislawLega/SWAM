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
            var user = DataContext as User;
            ApplicationDbContext context = new ApplicationDbContext();
            this.DataContext = context.Users.Include(u => u.Accesess)
                                             .Include(u => u.Emails)
                                             .Include(u => u.Phones)
                                             .FirstOrDefault(u => u.Id == user.Id);
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            TurnOffValues();
            TurnOnEditFields();
        }
      
        #region TurnOnEditFields
        /// <summary>
        /// Switch on all fields required to change user profile values.
        /// </summary>
        private void TurnOnEditFields()
        {
            SWAM.MainWindow.TurnOn(this.EditName);
            SWAM.MainWindow.TurnOn(this.EditPassword);
            SWAM.MainWindow.TurnOn(this.EditConfirmPassword);
            SWAM.MainWindow.TurnOn(this.EditPermissions);
        }
        #endregion
        #region TurnOffValues
        /// <summary>
        /// Turn off all uneditables values downloaded from database.
        /// </summary>
        private void TurnOffValues()
        {
            SWAM.MainWindow.TurnOff(this.Name);
            SWAM.MainWindow.TurnOff(this.Password);
            SWAM.MainWindow.TurnOff(this.Permissions);
        }
        #endregion


        private void DeletUser_Click(object sender, RoutedEventArgs e)
        {

        }

        #region BlockUser_Click
        /// <summary>
        /// Action after click button block user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make a window asking if you really want to block this user.
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = DataContext as User;
                var userDb = context.Users.FirstOrDefault(u => u.Id == user.Id);

                if (userDb != null )
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
        #endregion

        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var user = DataContext as User;
            var userType = (Enumerators.UserType)this.EditPermissions.SelectedValue;

            if (userType != user.Permissions)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions = userType;
                context.SaveChanges();

                Permissions.Text = context.Users.FirstOrDefault(u => u.Id == user.Id).Permissions.ToString();

                SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).
                    InformationForUser($"Upraweninia użytkownika {user.Name} zostały zmienione na {userType.ToString()}.");
            }
        }
    }
}
