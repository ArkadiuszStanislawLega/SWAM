
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    [Table("CustomerDeliveryAddress")]
    public class CustomerDeliveryAddress : Address
    {
        public Customer Customer { get; set; }
    }
}
