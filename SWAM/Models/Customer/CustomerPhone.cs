using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.Customer
{
    [Table("CustomersPhones")]
    public class CustomerPhone : Phone
    {
        public Customer Customer { get; set; }

        private ApplicationDbContext context = new ApplicationDbContext();


        public void Add()
        {
            var customer = context.People.OfType<Customer>().FirstOrDefault(c => c.Id == this.Customer.Id);
            CustomerPhone phone = new CustomerPhone()
            {
                Customer = customer,
                PhoneNumber = this.PhoneNumber
            };
            context.CustomersPhones.Add(phone);
            context.SaveChanges();
        }
    }
}
