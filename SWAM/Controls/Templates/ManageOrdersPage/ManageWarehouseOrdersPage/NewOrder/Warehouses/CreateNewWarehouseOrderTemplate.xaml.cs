using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Enumerators;
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

        private void CreateWarehouseOrder()
        {

        }
    }
}
