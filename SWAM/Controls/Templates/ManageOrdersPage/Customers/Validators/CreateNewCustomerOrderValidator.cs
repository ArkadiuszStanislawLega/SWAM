using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.Customer;

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
    }
}
