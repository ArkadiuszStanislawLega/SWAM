using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
        public string EmailAddress { get; set; }
        //TODO:Try catch block.
        private static ApplicationDbContext context = new ApplicationDbContext();

        #region IsNameExist
        /// <summary>
        /// checks the database to see if the courier name already exists.
        /// </summary>
        /// <param name="name">Name to check</param>
        /// <returns>True if name alredy exists in database.</returns>
        public static bool IsNameExist(string name)
        {
            if(name != string.Empty)
            {
                context = new ApplicationDbContext();
                if (context.Couriers.FirstOrDefault(c => c.Name == name) != null)
                    return true;
            }

            return false;
        }
        #endregion
        #region IsAdd
        /// <summary>
        /// Adding new customer to database.
        /// </summary>
        /// <param name="courier">New customer.</param>
        /// <returns>True if username does not exist in database.</returns>
        public bool IsAdd(Courier courier)
        {
            if (courier != null)
            {
                context.Couriers.Add(courier);

                var number = context.SaveChanges();
                if (number == 1)
                    return true;

            }
            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Returns the courier from the database by id number.
        /// </summary>
        /// <param name="Id">Courier id from database.</param>
        /// <returns>Courier from database.</returns>
        public static Courier Get(int Id)
        {
            context = new ApplicationDbContext();
            return context.People.OfType<Courier>().Include(c => c.CustomerOrders).FirstOrDefault(c => c.Id == Id);
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edit basic properties of courier. 
        /// </summary>
        /// <param name="editedCourier">Edited courier.</param>
        public void Edit(Courier editedCourier)
        {
            if (editedCourier != null)
            {
                var customer = Get(editedCourier.Id);
                customer.Name = editedCourier.Name;
                customer.Phone = editedCourier.Phone;
                customer.Tin = editedCourier.Tin;
                customer.EmailAddress = editedCourier.EmailAddress;
                context.SaveChanges();
            }
        }
        #endregion
    }
}
