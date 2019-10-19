using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            context = new ApplicationDbContext();
            return context.People.OfType<Customer>().Include(c => c.ResidentalAddress).FirstOrDefault(c => c.Id == Id);
        }
        #endregion
        #region Add
        /// <summary>
        /// Add new customer to database.
        /// </summary>
        /// <param name="customer">New customer.</param>
        public static void Add(Customer customer)
        {
            if (customer != null)
            {
                context.People.Add(customer);
                context.SaveChanges();
            }
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edit basic properties of customer. 
        /// Without Residental address.
        /// </summary>
        /// <param name="editedCustomer">Edited customer.</param>
        public void Edit(Customer editedCustomer)
        {
            if (editedCustomer != null)
            {
                var customer = Get(editedCustomer.Id);
                customer.Name = editedCustomer.Name;
                customer.Phone = editedCustomer.Phone;
                customer.Surname = editedCustomer.Surname;
                customer.EmailAddress = editedCustomer.EmailAddress;
                context.SaveChanges();
            }
        }
        #endregion
    }
}
