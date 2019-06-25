using SWAM.Controls.Templates.AdministratorPage.Warehouses;
using SWAM.Enumerators;
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
using SWAM.Controls.Templates.AdministratorPage;

namespace SWAM.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy WarehousesControlPanel.xaml
    /// </summary>
    public partial class WarehousesControlPanelTemplate : BasicUserControl
    {
        public readonly BookmarkInPage BookmarkAdministratorPage = BookmarkInPage.WarehousesControlPanel;

        private List<Warehouse> _warehouses;
        public Models.AdministratorPage.WarehousesListViewModel WarhousesListViewModel { get; set; }

        public WarehousesControlPanelTemplate()
        {
            InitializeComponent();

            WarhousesListViewModel = new Models.AdministratorPage.WarehousesListViewModel();
        }

        private void WarehousesControlPanelTemplate_Loaded(object sender, RoutedEventArgs e)
        {

            DataContext = WarhousesListViewModel;

            RefreshWarhousesList();
        }

        #region WarehousesList_SizeChanged
        /// <summary>
        /// Action when the main window has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehousesList_SizeChanged(object sender, SizeChangedEventArgs e)
        { 
            //TODO: Veryfy this 185.
            if (SWAM.MainWindow.IsMaximized) this.WarehousesList.MaxHeight = SystemParameters.PrimaryScreenHeight - 185;
            else this.WarehousesList.MaxHeight = SWAM.MainWindow.HeightOfAppliaction - 185;
        }
        #endregion

        #region RefreshWarhousesList
        /// <summary>
        /// Refreshing view model of warehouses list.
        /// </summary>
        public void RefreshWarhousesList()
        {
            if(WarhousesListViewModel.Size > 0) WarhousesListViewModel.RemoveAll();
            if (this._warehouses != null && this._warehouses.Count > 0) this._warehouses.Clear();

            using (ApplicationDbContext application = new ApplicationDbContext())
            {
                this._warehouses = application.Warehouses.ToList();
            };

            foreach (Warehouse w in this._warehouses)
                WarhousesListViewModel.AddWarehouse(w);
        }
        #endregion

        #region ShowPrfile
        /// <summary>
        /// Showing the profile of warehouse after
        /// after click item from the list with users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowProfile(WarehouseListItemTemplate warehouseListItemTemplate)
        {
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);

            //Items in list are tagget in userListItemTemplate by binding. Tag = UserId.
            this.RightSection.Children.Add(CreateWarehouseProfile((int)warehouseListItemTemplate.Tag));
        }
        #endregion

        #region CreateWarehouseProfile
        /// <summary>
        /// Made view of the warehouse profile in right section.
        /// </summary>
        /// <param name="warehouseIndexInWaregohouseList">Index number of WarehohousesListItemTemplate in the warehouses list.</param>
        /// <return>Chosen warehouse profile.</return>
        private WarehouseProfileTemplate CreateWarehouseProfile(int warehouseIndexInWaregohouseList)
        {
            var context = new ApplicationDbContext();
            return new WarehouseProfileTemplate() {
                DataContext = context.Warehouses.Include(a => a.Address).FirstOrDefault(w => w.Id == warehouseIndexInWaregohouseList)
            };
        }
        #endregion

        private void AddNewWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (this.RightSection.Children.Count > 0)
                this.RightSection.Children.RemoveAt(this.RightSection.Children.Count - 1);

            this.RightSection.Children.Add(new CreateNewWarehouseTemplate());
        }
    }
}
