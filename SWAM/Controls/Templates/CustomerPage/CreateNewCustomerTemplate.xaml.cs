﻿using SWAM.Models.Customer;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewCustomerTemplate.xaml
    /// </summary>
    public partial class CreateNewCustomerTemplate : UserControl
    {
        public CreateNewCustomerTemplate()
        {
            InitializeComponent();
            ResidentalAddress.ShowEditControls();
            DeliveryAddress.ShowEditControls();
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            var deliveryAddress = DeliveryAddress.GetCustomerDeliveryAddress();
            if (deliveryAddress != null)
            {
                Customer customer = new Customer()
                {
                    Name = CustomerName.Text,
                    Surname = CustomerSurname.Text,
                    EmailAddress = CustomerEmailAddress.Text,
                    Phone = CustomerPhone.Text,
                    ResidentalAddress = ResidentalAddress.GetCustomerResidenAddress(),
                    DeliveryAddress = deliveryAddress
                };
                Customer.Add(customer);
            }
            else
            {
                Customer customer = new Customer()
                {
                    Name = CustomerName.Text,
                    Surname = CustomerSurname.Text,
                    EmailAddress = CustomerEmailAddress.Text,
                    Phone = CustomerPhone.Text,
                    ResidentalAddress = ResidentalAddress.GetCustomerResidenAddress()
                };
                Customer.Add(customer);
            }

            CustomersListViewModel.Instance.Refresh();
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            CustomerName.Text = string.Empty;
            CustomerSurname.Text = string.Empty;
            CustomerEmailAddress.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            ResidentalAddress.ClearEditValues();
        }
    }
}
