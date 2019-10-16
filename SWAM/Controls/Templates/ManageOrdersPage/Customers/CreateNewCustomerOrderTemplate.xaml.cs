using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.Customer;
using SWAM.Models.ProductOrderList;
using SWAM.Models.User;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Interaction logic for CreateNewCustomerOrderTemplate.xaml
    /// </summary>
    public partial class CreateNewCustomerOrderTemplate : UserControl
    {
        public CreateNewCustomerOrderTemplate()
        {
            InitializeComponent();
        }

        private void SwitchContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (customerElementsContainer.Visibility == Visibility.Visible)
            {
                customerElementsContainer.Visibility = Visibility.Hidden;
                productElementsContainer.Visibility = Visibility.Visible;
                switchContentButton.Content = "Klient";
            }
            else
            {
                customerElementsContainer.Visibility = Visibility.Visible;
                productElementsContainer.Visibility = Visibility.Hidden;
                switchContentButton.Content = "Produkty";
            }
        }

        private void CreateCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            var context = new ApplicationDbContext();
            var customer = customerProfile.DataContext as Customer;
            var orderedProducts = new List<CustomerOrderPosition>(ProductOrderListViewModel.Instance.CustomerOrderPositions);
            var employee = context.People.OfType<User>().SingleOrDefault(p => p.Id == SWAM.MainWindow.LoggedInUser.Id);
            var employeeWarehouse = AccessUsersToWarehouses.GetUserAccess(SWAM.MainWindow.LoggedInUser.Id, context).Warehouse;

            var customerOrder = new CustomerOrder
            {
                IsPaid = false,
                OrderDate = DateTime.Now,
                CustomerOrderStatus = CustomerOrderStatus.InProcess,
                ShipmentType = ShipmentType.Reception,
                PaymentType = PaymentType.Postpaid,
                User = employee,
                Customer = customer,
                Courier = new Models.Courier.Courier(),
                Warehouse = employeeWarehouse,
                CustomerOrderPositions = orderedProducts
            };

            context.CustomerOrders.Add(customerOrder);
            context.SaveChanges();
        }
    }
}
