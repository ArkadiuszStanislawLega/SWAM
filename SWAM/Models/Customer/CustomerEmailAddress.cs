using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Customer
{
    [Table("CustomersEmailAddresses")]
    public class CustomerEmailAddress : EmailAddress
    {
        public Customer Customer { get; set; }
    }
}
