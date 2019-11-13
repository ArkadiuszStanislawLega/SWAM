using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
using System.Collections.Generic;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder.Customers.Validators
{
    public class CreateNewCustomerOrderValidator
    {
        public bool CustomerValidation(Customer customer)
        {
            if (customer == null)
            {
                return false;
            }
            return true;
        }

        public bool OrderedProductsValidation(List<CustomerOrderPosition> customerOrderPosition)
        {
            if (customerOrderPosition.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool CourierValidation(Courier courier)
        {
            if (courier == null)
            {
                return false;
            }
            return true;
        }

        public bool DeliveryAddressValidation(CustomerOrderDeliveryAddress customerOrderDeliveryAddress)
        {
            if (customerOrderDeliveryAddress == null)
                return false;

            if (customerOrderDeliveryAddress.ApartmentNumber == string.Empty
                || customerOrderDeliveryAddress.City == string.Empty
                || customerOrderDeliveryAddress.Country == string.Empty
                || customerOrderDeliveryAddress.HouseNumber == string.Empty
                || customerOrderDeliveryAddress.PostCode == string.Empty
                || customerOrderDeliveryAddress.Street == string.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
