using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing the client's <see cref="Person"/>.
    /// </summary>
    [Table("Couriers")]
    public class Courier : Person
    {
        /// <summary>
        /// Tax Identification Number
        /// </summary>
        public string Tin { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
        public IList<CustomerOrder> CustomerOrders { get; set; }
    }
}
