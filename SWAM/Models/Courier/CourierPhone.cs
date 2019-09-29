using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Courier
{
    [Table("CouriersPhones")]
    public class CourierPhone : Phone
    {
        public Courier Courier { get; set; }
    }
}
