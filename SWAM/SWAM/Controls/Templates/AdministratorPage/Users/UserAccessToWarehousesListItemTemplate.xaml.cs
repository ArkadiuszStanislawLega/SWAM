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
using SWAM.Enumerators;
using SWAM.Windows;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy UserAccessToWarehousesListItemTemplate.xaml
    /// </summary>
    public partial class UserAccessToWarehousesListItemTemplate : BasicUserControl
    {
        /// <summary>
        /// List with all warehouses available in database.
        /// </summary>
        private IList<Warehouse> _warehouses = Warehouse.GetAllWharehousesFromDb();
        #region Basic constructor
        public UserAccessToWarehousesListItemTemplate()
        {
            InitializeComponent();

            this.EditWarehouse.ItemsSource = _warehouses;
        }
        #endregion

        private void UserAccessToWarehousesListItemTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.DataContext is AccessUsersToWarehouses access)
                {
                    //TODO: Try - catch
                    var context = new ApplicationDbContext();
                    this.Calendar.SelectedDate = context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id).DateOfExpiredAcces;
                }
                else if (this.DataContext is User user) { /*TODO: debug this - it's work but it's weird*/ }
                else throw new RefreshWarehousessAccessesListExeption();
            }
            catch (RefreshWarehousessAccessesListExeption ex) { ex.ShowMessage(this); }
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
            this.DateOfGrantingAccess.Text = "" + DateTime.Now;
            this.Content.Margin = new Thickness(0, -10, 0, -10);
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
            if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this).DataContext is User user && this.EditWarehouse.SelectedValue is Warehouse warehouse)
            {
                var accessType = (Enumerators.UserType)this.EditUserPermissions.SelectedValue;

                if (AccessUsersToWarehouses.AddNewAccess(new AccessUsersToWarehouses()
                {
                    UserId = user.Id,
                    AdministratorId = SWAM.MainWindow.LoggedInUser.Id,
                    TypeOfAccess = accessType,
                    WarehouseId = warehouse.Id,
                    DateOfGrantingAccess = DateTime.Now,
                    DateOfExpiredAcces = this.Calendar.SelectedDate
                }))
                {
                    InformationToUser($"Dodano nowe uprawnienia {accessType.ToString()} użytkownikowi {user.Name} do magazynu {warehouse.Name}.");

                    this.EditWarehouse.SelectedValue = null;
                    this.EditUserPermissions.SelectedValue = null;
                    this.Calendar.SelectedDate = null;

                    RefreshParent();
                }
                else InformationToUser($"{ErrorMesages.DURING_ADD_ACCESS_TO_WAREHOUSE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
            }
            else InformationToUser(ErrorMesages.DURING_ADD_ACCESS_TO_WAREHOUSE_ERROR, true);
        }
        #endregion
        #region RefreshParent
        /// <summary>
        /// Refreshing parent container with user accesses.
        /// </summary>
        private void RefreshParent()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<UserAccessToWarehousesTemplates>(this) is UserAccessToWarehousesTemplates userAccessToWarehousesTemplates)
                    userAccessToWarehousesTemplates.RefreshAccessList();

                else throw new RefreshWarehousessAccessesListExeption(typeof(UserAccessToWarehousesListItemTemplate).ToString());
            }
            catch (RefreshWarehousessAccessesListExeption ex)
            {
                ex.ShowMessage(this);

            }
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
            if (SWAM.MainWindow.FindParent<UserProfileTemplate>(this).DataContext is User user)
            {
                if (this._confirmWindow != null)
                {
                    this._confirmWindow.Show($"Czy na pewno chcesz usunąć uprawnienia użytkownika {user.Name}?", out bool answer);

                    if (answer)
                    {
                        if (AccessUsersToWarehouses.RemoveAccess((int)this.Tag))
                        {
                            InformationToUser($"Uprawnienie {user.Name} zostało usunięte.");
                            RefreshParent();
                        }
                        else InformationToUser($"{ErrorMesages.DURING_DELETE_ACCESS_TO_WAREHOUSE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
                    }
                }
                else InformationToUser($"{ErrorMesages.DURING_DELETE_ACCESS_TO_WAREHOUSE_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_DELETE_ACCESS_TO_WAREHOUSE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion


        #region ConfirmExpiredDate_Click
        /// <summary>
        /// Command after adding/editing date of expired access to warehuse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmExpiredDate_Click(object sender, RoutedEventArgs e)
        {
            if (this.Calendar.SelectedDate != null && this.DataContext is AccessUsersToWarehouses access)
            {
                //Try - catch
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    access.EditExpiredAccess(this.Calendar.SelectedDate);

                    var newAccess = context.AccessUsersToWarehouses
                        .Include(u => u.User)
                        .Include(w => w.Warehouse)
                        .FirstOrDefault(a => a.Id == access.Id);

                    if (newAccess != null)
                    {
                        DataContext = newAccess;
                        InformationToUser($"Data wygaśnięcia uprawnienia {access.TypeOfAccess.ToString()} użytkownika {newAccess.User.Name} do magazynu {newAccess.Warehouse.Name} została edytowana.");
                    }
                    else InformationToUser($"{ErrorMesages.DURING_EDIT_ACCESS_TO_WAREHOUSE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
                }
            }
            else if (this.DataContext is User user) { /*TODO: debug this - it's work but it's weird*/}
            else InformationToUser($"{ErrorMesages.DURING_EDIT_ACCESS_TO_WAREHOUSE_ERROR}  {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
    }
}
