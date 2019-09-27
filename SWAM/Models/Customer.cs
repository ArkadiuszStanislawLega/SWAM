
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{   /// <summary>
    /// The basic model of the class in the database representing the courier  <see cref="Person"/>.
    /// </summary>
    [Table("Customers")]
    public class Customer : Person
    {
        /// <summary>
        /// Customer surname.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Customer's email address for contact.
        /// </summary>
        public EmailAddress EmailAddress { get; set; }
        /// <summary>
        /// Customer's residence address, for issuing a possible invoice.
        /// </summary>
        public Address ResidentAddress { get; set; }
        /// <summary>
        /// Delivery address of purchased materials.
        /// </summary>
        public Address DeliveryAddress { get; set; }
        /// <summary>
        /// All orders made by the customer.
        /// </summary>
        public IList<CustomerOrder> CustomerOrders { get; set; }
        /// <summary>
        /// All customer phone numbers.
        /// </summary>
        public IList<Phone> Phones { get; set; }
    }
}
