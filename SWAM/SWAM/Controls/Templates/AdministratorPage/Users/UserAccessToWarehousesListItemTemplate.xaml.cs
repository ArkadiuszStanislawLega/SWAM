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

            this.Loaded += (sender, e) => ShowPageAnimation(this).Begin();
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


        #region HidePageAnimation
        /// <summary>
        /// Animation for hide page.
        /// </summary>
        /// <param name="obj">Container whom is animating.</param>
        /// <returns></returns>
        protected Storyboard HidePageAnimation(DependencyObject obj)
        {
            Storyboard sb = new Storyboard();

            var opacityAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = 200,
                To = 0,
                DecelerationRatio = 1f
            };

            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = new Thickness(0),
                To = new Thickness(0, - 200, 0, 200),
                DecelerationRatio = 1f
            };

            Storyboard.SetTarget(slideAnimation, obj);
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            sb.Children.Add(slideAnimation);


            Storyboard.SetTarget(opacityAnimation, obj);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Height"));
            sb.Children.Add(opacityAnimation);

            return sb;
        }
        #endregion
        #region ShowPageAnimation
        /// <summary>
        /// Animation for show page/load.
        /// </summary>
        /// <param name="obj">Container whom is animating.</param>
        /// <returns></returns>
        protected Storyboard ShowPageAnimation(DependencyObject obj)
        {
            Storyboard sb = new Storyboard();

            var heightAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = 0,
                To = 200,
                DecelerationRatio = 1f
            };

            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.4)),
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0),
                DecelerationRatio = 1f
            };

            Storyboard.SetTarget(slideAnimation, obj);
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            sb.Children.Add(slideAnimation);


            Storyboard.SetTarget(heightAnimation, obj);
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath("Height"));
            sb.Children.Add(heightAnimation);

            return sb;
        }
        #endregion
    }
}
