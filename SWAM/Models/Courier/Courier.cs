using SWAM.Models.Customer;
using System.Collections.Generic;

namespace SWAM.Models.Courier
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
        /// <summary>
        /// List of customer orders that the courier delivers.
        /// </summary>
        public IList<CustomerOrder> CustomerOrders { get; set; }
        /// <summary>
        /// Phone number to courier.
        /// </summary>
        public string Phone { get; set; }
    }
}
