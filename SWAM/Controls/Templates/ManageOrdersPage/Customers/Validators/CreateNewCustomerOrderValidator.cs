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
                InformationToUser("Wybierz klienta z listy", true);
                return false;
            }
            return true;
        }
    }
}
