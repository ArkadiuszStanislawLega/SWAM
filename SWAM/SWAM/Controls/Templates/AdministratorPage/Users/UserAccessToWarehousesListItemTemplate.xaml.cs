using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.Entity;
using SWAM.Exceptions;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy UserAccessToWarehousesListItemTemplate.xaml
    /// </summary>
    public partial class UserAccessToWarehousesListItemTemplate : UserControl
    {
        /// <summary>
        /// List with all warehouses available in database.
        /// </summary>
        private IList<Warehouse> _warehouses = Warehouse.GetAllWharehousesFromDb();

        /// <summary>
        /// Information for user about all actions.
        /// </summary>
        private string _message;

        #region Basic constructor
        public UserAccessToWarehousesListItemTemplate()
        {
            InitializeComponent();

            this.EditWarehouse.ItemsSource = _warehouses;
        }
        #endregion

        private void UserAccessToWarehousesListItemTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is AccessUsersToWarehouses access)
            {
                //TODO: Try - catch
                ApplicationDbContext context = new ApplicationDbContext();
                this.Calendar.SelectedDate = context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id).DateOfExpiredAcces;
            }
        }
 
        #region CreateNewAccessMode
        /// <summary>
        /// Preapering view for add new access for user.
        /// </summary>
        public void CreateNewAccessMode()
        {
            this.EditUserPermissions.Visibility = Visibility.Visible;
            this.ConfirmAddAccess.Visibility = Visibility.Visible;
            this.EditWarehouse.Visibility = Visibility.Visible;

            this.DeleteCurrentAccess.Visibility = Visibility.Collapsed;

             this.AdministatorName.Text = SWAM.MainWindow.LoggedInUser.Name;
            this.DateOfGrantingAccess.Text = ""+DateTime.Now;
            this.Content.Margin = new Thickness(0, -10, 0 , -10);
        }
        #endregion

        #region NewCommand_Executed
        /// <summary>
        /// Command after adding/editing date of expired access to warehuse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Calendar.SelectedDate != null && this.DataContext is AccessUsersToWarehouses access)
            {
                //Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id).DateOfExpiredAcces = this.Calendar.SelectedDate;
                    context.SaveChanges();

                    var newAccess = context.AccessUsersToWarehouses
                        .Include(u => u.User)
                        .Include(w => w.Warehouse)
                        .FirstOrDefault(a => a.Id == access.Id);

                    if (newAccess != null)
                    {
                        DataContext = newAccess;
                        this._message = $"Data wygaśnięcia uprawnienia {access.TypeOfAccess.ToString()} użytkownika {newAccess.User.Name} do magazynu {newAccess.Warehouse.Name} została edytowana.";
                        InformationToUser();
                    }
                }
            }
        }
        #endregion  

        #region ConfirmAddAccess_Click
        /// <summary>
        /// Action after confirm button click to add new access.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmAddAccess_Click(object sender, RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this).DataContext is User user
                && this.EditWarehouse.SelectedValue is Warehouse warehouse)
            {
                var accessType = (Enumerators.UserType)this.EditUserPermissions.SelectedValue;

                //TODO: try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.AccessUsersToWarehouses.Add(new Models.AccessUsersToWarehouses()
                    {
                        UserId = user.Id,
                        AdministratorId = SWAM.MainWindow.LoggedInUser.Id,
                        TypeOfAccess = accessType,
                        WarehouseId = warehouse.Id,
                        DateOfGrantingAccess = DateTime.Now,
                        DateOfExpiredAcces = this.Calendar.SelectedDate
                    });
                    context.SaveChanges();
                }

                this.EditWarehouse.SelectedValue = null;
                this.EditUserPermissions.SelectedValue = null;
                this.Calendar.SelectedDate = null;

                var userAccessToWarehousesTemplates = RefreshParent();
                if (userAccessToWarehousesTemplates  != null)
                        userAccessToWarehousesTemplates.TurnOffAddNewAccess();

                this._message = $"Dodano nowe uprawnienia {accessType.ToString()} użytkownikowi {user.Name} do magazynu {warehouse.Name}.";
                InformationToUser();
            }
        }
        #endregion

        #region RefreshParent
        /// <summary>
        /// Refreshing parent container with user accesses.
        /// </summary>
        /// <returns>Parent <see cref="UserAccessToWarehousesTemplates"/>.</returns>
        private UserAccessToWarehousesTemplates RefreshParent()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UserAccessToWarehousesTemplates>(this) is UserAccessToWarehousesTemplates userAccessToWarehousesTemplates)
                {
                    userAccessToWarehousesTemplates.RefreshAccessList();
                    return userAccessToWarehousesTemplates;
                }
                else throw new RefreshWarehousessAccessesListExeption(typeof(UserAccessToWarehousesListItemTemplate).ToString());  
            }
            catch (RefreshWarehousessAccessesListExeption ex)
            {
                ex.ShowMessage(this);
                return null;
            }
        }
        #endregion
        #region InformationToUser
        /// <summary>
        /// Make information in MainWindow to user about action.
        /// </summary>
        private void InformationToUser()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                    mainWindow.InformationForUser(this._message);
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex) { ex.ShowMessage(this); }
        }
        #endregion

        #region Delete_Click
        /// <summary>
        /// Action after click delete access button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make a window asking if you really want to delete this permission.
            //TODO: Try - catch
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var accessToRemove = context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == (int)this.Tag);

                if (accessToRemove != null)
                {
                    context.AccessUsersToWarehouses.Remove(accessToRemove);
                    context.SaveChanges();

                    if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this).DataContext is User user)
                    {
                        this._message = $"Uprawnienie  {user.Name} zostało usunięte.";
                        InformationToUser();
                    }
                }
            }
            RefreshParent();
        }
        #endregion
    }
}
