using System.Collections.Generic;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing the client's <see cref="Person"/>.
    /// </summary>
    public class Courier : Person
    {
        /// <summary>
        /// Tax Identification Number
        /// </summary>
        public string Tin { get; set; }
        public IList<CustomerOrder> CustomerOrders { get; set; }
    }
}
