using System;
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
        public string Phone { get; set; }
        /// <summary>
        /// Customer's email address for contact.
        /// </summary>
        public string EmailAddress { get; set; }
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
        public CustomerResidentalAddress ResidentalAddress { get; set; }

        private static ApplicationDbContext context = new ApplicationDbContext();

        #region Get
        /// <summary>
        /// Returns the client from the database by id number.
        /// </summary>
        /// <param name="Id">Client id from database.</param>
        /// <returns>Client from database.</returns>
        public static Customer Get(int Id)
        {
            throw new NotImplementedException();
        }
        #endregion  

        public static void Add(Customer customer)
        {
            if (customer != null)
            {
                context.People.Add(customer);
                context.SaveChanges();
            }
        }
    }
}
