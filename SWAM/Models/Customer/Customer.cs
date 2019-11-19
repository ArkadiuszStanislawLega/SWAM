using SWAM.Strings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace SWAM.Models.Customer
{   /// <summary>
    /// The basic model of the class in the database representing the courier  <see cref="Person"/>.
    /// </summary>
    public class Customer 
    {
        /// <summary>
        /// The id number of the person in the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// First name of the person.
        /// </summary>
        public string Name { get; set; }
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
        #region Database connection
        private static ApplicationDbContext context = new ApplicationDbContext();

        public static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return context;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => context = value;
        }
        #endregion
        #region Get
        /// <summary>
        /// Returns the client from the database by id number.
        /// </summary>
        /// <param name="Id">Client id from database.</param>
        /// <returns>Client from database.</returns>
        public static Customer Get(int Id)
        {
            Context = new ApplicationDbContext();
            return Context.Customers.Include(c => c.ResidentalAddress).FirstOrDefault(c => c.Id == Id);
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
                Context.Customers.Add(customer);
                Context.SaveChanges();
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
                customer.Surname = editedCustomer.Surname;
                customer.Phone = editedCustomer.Phone;
                customer.EmailAddress = editedCustomer.EmailAddress;
                Context.SaveChanges();
            }
        }
        #endregion
    }
}
