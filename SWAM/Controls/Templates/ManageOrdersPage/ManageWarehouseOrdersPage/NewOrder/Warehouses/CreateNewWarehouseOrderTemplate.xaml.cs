using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses.Validators;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.ExternalSupplier;
using SWAM.Models.User;
using SWAM.Models.ViewModels.CreateNewWarehouseOrder;
using SWAM.Models.Warehouse;
using System;
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
                //if (!(SWAM.MainWindow.LoggedInUser.Permissions == UserType.Manager))
                //{
                //    InformationToUser("Niewystarczający poziom uprawnień", true);
                //    return;
                //}
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

            if (!validator.ExternalSupplierValidation(externalSupplier))
            {
                InformationToUser("Wybierz dostawcę z listy", true);
                return;
            }

            if (!validator.OrderedProductsValidation(orderedProducts))
            {
                InformationToUser("Lista zamówień jest pusta", true);
                return;
            }

            if (!validator.WarehouseValidator(warehouse))
            {
                InformationToUser("Wybierz magazyn z listy", true);
                return;
            }

            var employee = context.People.OfType<User>().SingleOrDefault(p => p.Id == SWAM.MainWindow.LoggedInUser.Id);

            var warehouseOrder = new WarehouseOrder
            {
                IsPaid = true,
                OrderDate = DateTime.Now,
                WarehouseId = warehouse.Id,
                CreatorId = employee.Id,
                ExternalSupplayerId = externalSupplier.Id,
                WarehouseOrderStatus = WarehouseOrderStatus.Ordered
            };

            context.WarehouseOrders.Add(warehouseOrder);
            context.SaveChanges();

            // Add warehouse order positions
            var customerOrderPositionsFromDb = new List<WarehouseOrderPosition>();
            foreach (var item in orderedProducts)
            {
                customerOrderPositionsFromDb.Add(new WarehouseOrderPosition()
                {
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Product = context.Products.SingleOrDefault(s => s.Id == item.ProductId),
                    ProductId = context.Products.SingleOrDefault(s => s.Id == item.ProductId).Id
                });
            }

            foreach (var item in customerOrderPositionsFromDb)
            {
                item.WarehouseOrder = warehouseOrder;
                context.WarehouseOrderPositions.Add(item);
            }

            context.SaveChanges();

            InformationToUser("Dodano zamówienie", false);

            warehouseProfile.DataContext = null;
            externalSupplierProfile.DataContext = null;
            ProductOrderListViewModel.Instance.Clear();

            visiblePage = WarehouseOrderVisiblePage.WarehousePage;
            SwitchPagesVisibility();
        }
        #endregion
    }
}
