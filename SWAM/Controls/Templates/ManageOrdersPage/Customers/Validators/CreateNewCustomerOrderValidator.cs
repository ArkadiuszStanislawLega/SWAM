using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.Customer;
using System.Collections.Generic;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers.Validators
{
    public class CreateNewCustomerOrderValidator : BasicUserControl
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
    }
}
