using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses.Validators;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.ExternalSupplier;
using SWAM.Models.ViewModels.CreateNewWarehouseOrder;
using SWAM.Models.Warehouse;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewWarehouseOrderTemplate.xaml
    /// </summary>
    public partial class CreateNewWarehouseOrderTemplate : BasicUserControl
    {
        #region Properties
        // Current visible page
        WarehouseOrderVisiblePage visiblePage = WarehouseOrderVisiblePage.WarehousePage;
        // Pages with its grid containers elements
        Dictionary<WarehouseOrderVisiblePage, ScrollViewer> pages;
        #endregion

        #region Constructor
        public CreateNewWarehouseOrderTemplate()
        {
            InitializeComponent();

            // Seed dictionary
            pages = new Dictionary<WarehouseOrderVisiblePage, ScrollViewer>()
            {
                { WarehouseOrderVisiblePage.WarehousePage, warehouseElementsContainer},
                { WarehouseOrderVisiblePage.ProductPage, productElementsContainer},
                { WarehouseOrderVisiblePage.ExternalSupplierPage, externalSupplierElementsContainer}
            };
        }
        #endregion

        #region SwitchContentPreviousButton_Click
        /// <summary>
        /// Switches to previous tab 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchContentPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if current page is first
            if (visiblePage == WarehouseOrderVisiblePage.WarehousePage)
                return;

            // Check if current page is last 
            if (pages.Last().Key == visiblePage)
                switchContentNextButton.Content = "Następny";

            // Decrease visibile page element
            visiblePage--;

            // Switch pages visibility
            SwitchPagesVisibility();
        }
        #endregion

        #region SwitchContentNextButton_Click
        /// <summary>
        /// Switches to next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchContentNextButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if current page is last
            if (pages.Last().Key == visiblePage)
            {
                // Submit form
                CreateWarehouseOrder();
                return;
            }

            // Check if current page is second last 
            if (pages.Count - 2 == (int)visiblePage)
                switchContentNextButton.Content = "Zatwierdź";

            // Increase visibile page element
            visiblePage++;

            // Switch pages visibility
            SwitchPagesVisibility();
        }
        #endregion

        #region SwitchPagesVisibility
        private void SwitchPagesVisibility()
        {
            foreach (var page in pages)
            {
                if (page.Key != visiblePage)
                {
                    page.Value.Visibility = Visibility.Hidden;
                }
                else
                {
                    page.Value.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion

        #region CreateWarehouseOrder
        /// <summary>
        /// Gets form values and creates warehouse order
        /// </summary>
        private void CreateWarehouseOrder()
        {
            var context = new ApplicationDbContext();
            var warehouse = warehouseProfile.DataContext as Warehouse;
            var externalSupplier = externalSupplierProfile.DataContext as ExternalSupplier;
            var orderedProducts = new List<WarehouseOrderPosition>(ProductOrderListViewModel.Instance.WarehouseOrderPositions);

            var validator = new CreateNewWarehouseOrderValidator();

            if (!validator.WarehouseValidator(warehouse))
            {
                InformationToUser("Wybierz magazyn z listy", true);
                return;
            }

        }
        #endregion
    }
}
