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
using System.Windows.Media.Animation;

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
        }

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
        private void AddNewAcces_Click(object sender, RoutedEventArgs e) => this.AddNewAccess.CreateNewAccessMode();
        #endregion
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
        #region HideAddNewAccess
        /// <summary>
        /// Hide new access view.
        /// </summary>
        public void HideAddNewAccess()
        {
            if(TryFindResource("HideAddNewAccessStory") is Storyboard story) story.Begin();
        }
        #endregion
    }
}
