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

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    /// <summary>
    /// Logika interakcji dla klasy UserAccessToWarehousesTemplates.xaml
    /// </summary>
    public partial class UserAccessToWarehousesTemplates : UserControl
    {
        public UserAccessToWarehousesTemplates()
        {
            InitializeComponent();
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            RefreshAccessList();
        }

        private void AddNewAcces_Click(object sender, RoutedEventArgs e)
        {
            this.AddNewAccess.CreateNewAccessMode();
            SWAM.MainWindow.TurnOn(this.AddNewAccess);
        }

        public void TurnOffAddNewAccess()
        {
            AddNewAccess.Visibility = Visibility.Collapsed;
            //TODO: Do it to clear the fields as it would at the beginning - Adding access
        }

        #region RefreshPhoneList
        /// <summary>
        /// Refreshing view list of accesses.
        /// </summary>
        public void RefreshAccessList()
        {
            var user = DataContext as User;

            var context = new ApplicationDbContext();
            List.ItemsSource = context.AccessUsersToWarehouses.Where(u => u.UserId == user.Id).ToList();
        }
        #endregion
    }
}
