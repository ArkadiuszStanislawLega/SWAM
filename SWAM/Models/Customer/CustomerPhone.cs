using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Customer
{
    [Table("CustomersPhones")]
    public class CustomerPhone : Phone
    {
        public Customer Customer { get; set; }
    }
}
