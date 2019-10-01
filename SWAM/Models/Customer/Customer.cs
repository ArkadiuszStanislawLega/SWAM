using System.Collections.Generic;

namespace SWAM.Models.Customer
{   /// <summary>
    /// The basic model of the class in the database representing the courier  <see cref="Person"/>.
    /// </summary>
    public class Customer : Person
    {
        /// <summary>
        /// customer's lastname
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// customer's phone number.
        /// </summary>
        public CustomerPhone Phone { get; set; }
        /// <summary>
        /// Customer's email address for contact.
        /// </summary>
        public CustomerEmailAddress EmailAddress { get; set; }
        /// <summary>
        /// All orders made by the customer.
        /// </summary>
        public IList<CustomerOrder> Orders { get; set; }
        /// <summary>
        /// Customer delivery address.
        /// </summary>
        public CustomerDeliveryAddress DeliveryAddress { get; set; }
        /// <summary>
        /// Customer residental address.
        /// </summary>
        public CusomterResidentalAddress ResidentalAddress { get; set; }
    }
}
