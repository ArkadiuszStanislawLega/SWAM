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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using SWAM.Exceptions;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy UserAccessToWarehousesTemplates.xaml
    /// </summary>
    public partial class UserAccessToWarehousesTemplates : BasicUserControl
    {
        public UserAccessToWarehousesTemplates()
        {
            InitializeComponent();
            //Showing scroll viewer after loaded animation of list of accesses to warehouses has been finished. 
            this.LoadedAnimation.Completed += (sender, e) => FitViewToFillInParent();
        }

        #region FitViewToFillInParent
        /// <summary>
        /// Changes the size of the scroll viewer appearance to a size that matches the size of the parent.
        /// It's counting free space between title of the list and bottom position of current first parent.
        /// Parent must be <see cref="UserProfileTemplate"/>class in <see cref="UsersControlPanelTemplate"/> class.
        /// </summary>
        public void FitViewToFillInParent()
        {
            //Finding parent of the parent...
            var parent = SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this) as UsersControlPanelTemplate;
            //finding first parent...
            if (parent != null && parent.RightSection != null && parent.RightSection.Children[0] != null)
            {
                var profile = parent.RightSection.Children[0] as UserProfileTemplate;

                //Counting max height of the scroll viewer
                //Is needed to know how big is our parent of the first parent, then we deduct size of titles. 
                //So now we have our space between title of the list and bottom possition of current first parent.
                //Number 30 it is sum sizes of margins.
                profile.AccesToWarehousesList.ListScroll.MaxHeight = parent.ActualHeight - profile.AccesToWarehousesList.Title.ActualHeight
                    - profile.Title.ActualHeight - 30;
            }
        }
        #endregion
        #region OnVisualChildrenChanged
        /// <summary>
        /// When new user acces to warehouses is added is needed to refresh data in list.
        /// </summary>
        /// <param name="visualAdded"></param>
        /// <param name="visualRemoved"></param>
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            RefreshAccessList();
        }
        #endregion
        #region AddNewAcces_Click
        /// <summary>
        /// Action after click add new access.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewAcces_Click(object sender, RoutedEventArgs e)
        {
            this.AddNewAccess.CreateNewAccessMode();
            SWAM.MainWindow.TurnOn(this.AddNewAccess);
        }
        #endregion

        public void TurnOffAddNewAccess()
        {
            AddNewAccess.Visibility = Visibility.Collapsed;
            //TODO: Do it to clear the fields as it would at the beginning - Adding access
        }

        #region RefreshAccessList
        /// <summary>
        /// Refreshing view list of accesses.
        /// </summary>
        public void RefreshAccessList()
        {
            try
            {
                if (DataContext is User user)
                {
                    var accesses = AccessUsersToWarehouses.GetUserAccesses(user.Id);
                    if (accesses != null)
                        List.ItemsSource = accesses;
                    else throw new RefreshWarehousessAccessesListExeption();
                }
                else throw new RefreshWarehousessAccessesListExeption();
            }
            catch(RefreshWarehousessAccessesListExeption ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
