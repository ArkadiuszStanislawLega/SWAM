using System.Collections.Generic;

namespace SWAM.Models
{   /// <summary>
    /// The basic model of the class in the database representing the courier  <see cref="Person"/>.
    /// </summary>
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
        public PersonAddresses ResidentAddress { get; set; }
        /// <summary>
        /// Delivery address of purchased materials.
        /// </summary>
        public PersonAddresses DeliveryAddress { get; set; }
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
