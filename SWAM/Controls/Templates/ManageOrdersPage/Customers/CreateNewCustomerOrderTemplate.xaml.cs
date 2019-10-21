using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
using SWAM.Models.ProductOrderList;
using SWAM.Models.User;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Interaction logic for CreateNewCustomerOrderTemplate.xaml
    /// </summary>
    public partial class CreateNewCustomerOrderTemplate : UserControl
    {
        #region Properties
        // Current visible page
        CustomerOrderVisiblePage visiblePage = CustomerOrderVisiblePage.CustomerPage;
        // Pages with its grid containers elements
        Dictionary<CustomerOrderVisiblePage, Grid> pages;
        #endregion

        #region Constructor
        public CreateNewCustomerOrderTemplate()
        {
            InitializeComponent();

            // Seed dictionary
            pages = new Dictionary<CustomerOrderVisiblePage, Grid>()
            {
                { CustomerOrderVisiblePage.CustomerPage, customerElementsContainer},
                { CustomerOrderVisiblePage.ProductPage, productElementsContainer},
                { CustomerOrderVisiblePage.CourierPage, courierElementsContainer}
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
            if (visiblePage == CustomerOrderVisiblePage.CustomerPage)
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
                CreateCustomerOrder();
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

        #region IsPersonalCollected_Click
        /// <summary>
        /// Clears courier's form data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsPersonalCollected_Click(object sender, RoutedEventArgs e)
        {
            if (isPersonalCollected.IsChecked.GetValueOrDefault())
            {
                DataContext = null;
                courierListProfile.couriersListView.UnselectAll(); 
            }
        }
        #endregion

        #region CreateCustomerOrder
        /// <summary>
        /// Gets form values and creates customer order
        /// </summary>
        private void CreateCustomerOrder()
        {
            var context = new ApplicationDbContext();
            var customer = customerProfile.DataContext as Customer;
            var orderedProducts = new List<CustomerOrderPosition>(ProductOrderListViewModel.Instance.CustomerOrderPositions);
            var employee = context.People.OfType<User>().SingleOrDefault(p => p.Id == SWAM.MainWindow.LoggedInUser.Id);
            var employeeWarehouse = AccessUsersToWarehouses.GetUserAccess(SWAM.MainWindow.LoggedInUser.Id, context).Warehouse;

            orderedProducts.ForEach(p => { if (p.Product != null) context.Products.Attach(p.Product); });

            var customerOrder = new CustomerOrder
            {
                IsPaid = false,
                OrderDate = DateTime.Now,
                CustomerOrderStatus = CustomerOrderStatus.InProcess,
                ShipmentType = ShipmentType.Reception,
                PaymentType = PaymentType.Postpaid,
                UserId = employee.Id,
                CustomerId = customer.Id,
                WarehouseId = employeeWarehouse.Id,
                CustomerOrderPositions = orderedProducts
            };

            context.CustomerOrders.Add(customerOrder);
            context.SaveChanges();
        }
        #endregion
    }
}
