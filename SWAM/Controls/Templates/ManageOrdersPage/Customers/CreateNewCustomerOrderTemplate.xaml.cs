using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.ManageOrdersPage.Customers.Validators;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
using SWAM.Models.ManageOrdersPage;
using SWAM.Models.ProductOrderList;
using SWAM.Models.User;
using SWAM.Models.ViewModels.CreateNewCustomerOrder;
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
    public partial class CreateNewCustomerOrderTemplate : BasicUserControl
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
                courierProfile.DataContext = null;
                courierProfile.SetFieldsGray();
                customerOrderAddress.Visibility = Visibility.Hidden;
                personalAddressCollection.Visibility = Visibility.Visible;
                courierListProfile.couriersListView.UnselectAll();
            }
            else
            {
                courierProfile.SetFieldsWhite();
                customerOrderAddress.Visibility = Visibility.Visible;
                personalAddressCollection.Visibility = Visibility.Hidden;
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
            var courier = courierProfile.DataContext as Courier;
            var deliveryAddress = deliveryAddressProfile.GetCustomerOrderDeliveryAddress();
            var orderedProducts = new List<CustomerOrderPosition>(ProductOrderListViewModel.Instance.CustomerOrderPositions);
            ShipmentType shipmentType = ShipmentType.Reception;

            var validator = new CreateNewCustomerOrderValidator();

            // Get all radio buttons 
            List<RadioButton> radioButtons = paymentTypes.Children.OfType<RadioButton>().ToList();
            // Search list for checked radio button
            RadioButton chekedRadioButton = radioButtons.Where(rb => rb.IsChecked == true).Single();

            if ((bool)(!isPersonalCollected.IsChecked))
            {
                if (!validator.CourierValidation(courier))
                {
                    InformationToUser("Wybierz kuriera z listy", true);
                    return;
                }

                if (!validator.DeliveryAddressValidation(deliveryAddress))
                {
                    InformationToUser("Uzupełnij lub popraw adres dostawy", true);
                    return;
                }

                shipmentType = ShipmentType.Shipment;
            }

            if (!validator.OrderedProductsValidation(orderedProducts))
            {
                InformationToUser("Lista zamówień jest pusta", true);
                return;
            }

            if (!validator.CustomerValidation(customer))
            {
                InformationToUser("Wybierz klienta z listy", true);
                return;
            }

            orderedProducts.ForEach(p => { if (p.Product != null) context.Products.Attach(p.Product); });

            var employee = context.People.OfType<User>().SingleOrDefault(p => p.Id == SWAM.MainWindow.LoggedInUser.Id);
            var employeeWarehouse = UserDependsAccessProductListViewModel.Instance.States.ElementAtOrDefault(0).WarehouseId;

            // Create customer order based on forms data
            var customerOrder = new CustomerOrder
            {
                IsPaid = false,
                OrderDate = DateTime.Now,
                ShipmentType = shipmentType,
                PaymentType = (PaymentType)(int.Parse(chekedRadioButton.Tag.ToString())),
                UserId = employee.Id,
                CustomerId = customer.Id,
                WarehouseId = employeeWarehouse,
                CustomerOrderPositions = orderedProducts
            };

            // Add order status based on selected pay method
            if ((PaymentType)(int.Parse(chekedRadioButton.Tag.ToString())) == PaymentType.Postpaid)
            {
                customerOrder.CustomerOrderStatus = CustomerOrderStatus.InProcess;
            }
            else if ((PaymentType)(int.Parse(chekedRadioButton.Tag.ToString())) == PaymentType.Prepaid)
            {
                customerOrder.CustomerOrderStatus = CustomerOrderStatus.WaitingForPayment;
            }

            // Add address only if package is not personaly collected by customer
            if ((bool)(!isPersonalCollected.IsChecked))
            {
                customerOrder.DeliveryAddress = deliveryAddress;
            }

            // Decrease quantity of products in states that have been purchased from
            foreach (var item in orderedProducts)
            {
                var state = context.States.FirstOrDefault(s => s.Id == item.State.Id);
                state.Quantity -= item.Quantity;
                state.Available -= item.Quantity;
                context.SaveChanges();
            }

            context.CustomerOrders.Add(customerOrder);
            context.SaveChanges();

            InformationToUser("Dodano zamówienie", false);

            customerProfile.DataContext = null;
            courierProfile.DataContext = null;
            ProductOrderListViewModel.Instance.Clear();
            PaymentOrderViewModel.Instance.Refresh();
            UserDependsAccessProductListViewModel.Instance.Refresh();
        }
        #endregion
    }
}
