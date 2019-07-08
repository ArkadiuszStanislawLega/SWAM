﻿using SWAM.Controls.Templates.AdministratorPage.Warehouses;
using SWAM.Enumerators;
using SWAM.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using SWAM.Controls.Templates.AdministratorPage;
using System.ComponentModel;

namespace SWAM.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy WarehousesControlPanel.xaml
    /// </summary>
    public partial class WarehousesControlPanelTemplate : BasicUserControl
    {
        public readonly BookmarkInPage BookmarkAdministratorPage = BookmarkInPage.WarehousesControlPanel;

        private Models.AdministratorPage.WarehousesListViewModel warhousesListViewModel { get; set; } = new Models.AdministratorPage.WarehousesListViewModel();

        public WarehousesControlPanelTemplate()
        {
            InitializeComponent();
        }

        private void WarehousesControlPanelTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            warhousesListViewModel.Refresh();

            DataContext = warhousesListViewModel;
        }

        public void RefreshList() => warhousesListViewModel.Refresh();

        #region WarehousesList_SizeChanged
        /// <summary>
        /// Action when the main window has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehousesList_SizeChanged(object sender, SizeChangedEventArgs e)
        { 
            //TODO: Veryfy this 185.
            //if (SWAM.MainWindow.IsMaximized) this.WarehousesList.MaxHeight = SystemParameters.PrimaryScreenHeight - 185;
            //else this.WarehousesList.MaxHeight = SWAM.MainWindow.HeightOfAppliaction - 185;
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

        #region TextBox_TextChanged
        /// <summary>
        /// Filtering list depends on text typed in TextBox named FindWarehouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //filter is required observable collection.
            ICollectionView filter = CollectionViewSource.GetDefaultView(warhousesListViewModel.WarehousesList);
            filter.Filter = warehouse =>
            {
                Warehouse allWarehousesWhose = warehouse as Warehouse;
                return allWarehousesWhose.Name.Contains(FindWarehouse.Text);
            };
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after click checkBox in filters container to change type of sorting(ascending/descending) user list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (WarehousesList.Items.SortDescriptions.Count > 0)
                WarehousesList.Items.SortDescriptions.RemoveAt(WarehousesList.Items.SortDescriptions.Count - 1);

            if (SortAscending.IsChecked == true)
                WarehousesList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            else
                WarehousesList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion
    }
}
