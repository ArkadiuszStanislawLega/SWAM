using SWAM.Models.Customer;
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
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer()
            {
                Name = CustomerName.Text,
                Surname = CustomerSurname.Text,
                Phone = new CustomerPhone()
                {
                    PhoneNumber = CustomerPhone.Text
                },
                EmailAddress = new CustomerEmailAddress()
                {
                    AddressEmail = CustomerEmailAddress.Text
                },
                ResidentalAddress = ResidentalAddress.GetCustomerResidenAddress()
            };
            Customer.Add(customer);
        }
    }
}
