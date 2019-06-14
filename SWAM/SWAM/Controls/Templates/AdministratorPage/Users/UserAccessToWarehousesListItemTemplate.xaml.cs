using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy UserAccessToWarehousesListItemTemplate.xaml
    /// </summary>
    public partial class UserAccessToWarehousesListItemTemplate : UserControl
    {
        private IList<Warehouse> _warehouses;

        public UserAccessToWarehousesListItemTemplate()
        {
            //Create list of warehouses in database - is required to add new access for user.
            _warehouses = Warehouse.GetAllWharehousesFromDb();

            InitializeComponent();

            this.EditWarehouse.ItemsSource = _warehouses;
        }
        #region CreateNewAccessMode
        /// <summary>
        /// Preapering view for add new access for user.
        /// </summary>
        public void CreateNewAccessMode()
        {
            SWAM.MainWindow.TurnOn(this.EditUserPermissions);
            SWAM.MainWindow.TurnOn(this.ConfirmAddAccess);
            SWAM.MainWindow.TurnOn(this.EditWarehouse);

            this.AdministatorName.Text = SWAM.MainWindow.LoggedInUser.Name;
            this.DateOfGrantingAccess.Text = ""+DateTime.Now;
            this.Content.Margin = new Thickness(0, -10, 0 , -10);
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
            var user = SWAM.MainWindow.FindParent<UserProfileTemplate>(this).DataContext as User;
            var warehouse = this.EditWarehouse.SelectedValue as Warehouse;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.AccessUsersToWarehouses.Add(new Models.AccessUsersToWarehouses()
                {
                    UserId = user.Id,
                    AdministratorId = SWAM.MainWindow.LoggedInUser.Id,
                    TypeOfAccess = (Enumerators.UserType)this.EditUserPermissions.SelectedValue,
                    WarehouseId = warehouse.Id,
                    DateOfGrantingAccess = DateTime.Now

                    //TODO: Add calendar to set date of expire access
                });
                context.SaveChanges();
            }
            UserAccessToWarehousesTemplates userAccessToWarehousesTemplates = SWAM.MainWindow.FindParent<UserAccessToWarehousesTemplates>(this);
            userAccessToWarehousesTemplates.RefreshAccessList();
            userAccessToWarehousesTemplates.TurnOffAddNewAccess();
        }
        #endregion
    }
}
