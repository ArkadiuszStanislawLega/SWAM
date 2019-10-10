using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SWAM.Models.Customer
{
    [Table("CustomersEmailAddresses")]
    public class CustomerEmailAddress : EmailAddress
    {
        public Customer Customer { get; set; } 

        private ApplicationDbContext context = new ApplicationDbContext();

        public void Add()
        {
            var customer = context.People.OfType<Customer>().FirstOrDefault(c => c.Id == this.Customer.Id);
            CustomerEmailAddress emailAddress = new CustomerEmailAddress()
            {
                Customer = customer,
                AddressEmail = this.AddressEmail
            };
            context.CustomerEmailAddresses.Add(emailAddress);
            context.SaveChanges();
        }
    }
}
