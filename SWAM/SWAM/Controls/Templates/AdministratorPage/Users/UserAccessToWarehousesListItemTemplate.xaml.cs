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
        /// <summary>
        /// List with all warehouses available in database.
        /// </summary>
        private IList<Warehouse> _warehouses;

        #region Basic constructor
        public UserAccessToWarehousesListItemTemplate()
        {
            //Create list of warehouses in database - is required to add new access for user.
            _warehouses = Warehouse.GetAllWharehousesFromDb();

            InitializeComponent();

            this.EditWarehouse.ItemsSource = _warehouses;
        }
        #endregion

        #region Overrided
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var access = this.DataContext as AccessUsersToWarehouses;
            if (access != null)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                this.Calendar.SelectedDate = context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id).DateOfExpiredAcces;
            }
        }
        #endregion

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

        #region NewCommand_Executed
        /// <summary>
        /// Command after adding/editing date of expired access to warehuse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(this.Calendar.SelectedDate != null)
            {
                var access = this.DataContext as AccessUsersToWarehouses;
                if (access != null)
                {
                    ApplicationDbContext context = new ApplicationDbContext();
                    context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id).DateOfExpiredAcces = this.Calendar.SelectedDate;
                    context.SaveChanges();

                    DataContext = context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == access.Id);
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
                    DateOfGrantingAccess = DateTime.Now,
                    DateOfExpiredAcces = this.Calendar.SelectedDate
                });
                context.SaveChanges();
            }

            this.EditWarehouse.SelectedValue = null;
            this.EditUserPermissions.SelectedValue = null;
            this.Calendar.SelectedDate = null;

            var userAccessToWarehousesTemplates = SWAM.MainWindow.FindParent<UserAccessToWarehousesTemplates>(this);
            userAccessToWarehousesTemplates.RefreshAccessList();
            userAccessToWarehousesTemplates.TurnOffAddNewAccess();
        }
        #endregion
    }
}
