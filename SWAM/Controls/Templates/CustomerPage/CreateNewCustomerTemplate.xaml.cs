using SWAM.Models.Customer;
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
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
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
            CustomersListViewModel.Instance.Refresh();
        }
    }
}
