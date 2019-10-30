using SWAM.Models.Customer;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for DeliveryAddressFormTemplate.xaml
    /// </summary>
    public partial class DeliveryAddressFormTemplate : UserControl
    {
        public DeliveryAddressFormTemplate()
        {
            InitializeComponent();
        }

        public CustomerOrderDeliveryAddress GetCustomerOrderDeliveryAddress()
        {
            return new CustomerOrderDeliveryAddress {
                Country = Country.Text,
                City = City.Text,
                ApartmentNumber = ApartmentNumber.Text,
                Street = Street.Text,
                HouseNumber = HouseNumber.Text,
                PostCode = PostCode.Text
            };
        }
    }
}
