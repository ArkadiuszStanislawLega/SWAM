using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Customer
{
    [Table("CustomersDeliveryAddresses")]
    public class CustomerDeliveryAddress : Address
    {
        public Customer Customer { get; set; }
    }
}
