using SWAM.Models.Warehouse;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.ExternalSupplier
{
    /// <summary>
    /// The basic model of the class in the database representing the external supplier to the warehouses.
    /// </summary>
    public class ExternalSupplier : Person
    {
        /// <summary>
        /// Tax Identification Number.
        /// </summary>
        public string Tin { get; set; }

        public ExternalSupplierAddress Address { get; set; }

        public List<ExternalSupplierPhone> Phones { get; set; }

        public ExternalSupplierEmailAddress EmailAddress { get; set; }

        public IList<WarehouseOrder> WarehouseOrders { get; set; }
        //TODO: Try catch block
        private static ApplicationDbContext context = new ApplicationDbContext();

        #region Get
        /// <summary>
        /// Resived externa supplier from database.
        /// </summary>
        /// <param name="Id">External supplier number Id in database.</param>
        /// <returns>External supplier from database.</returns>
        public static ExternalSupplier Get(int Id)
        {
            context = new ApplicationDbContext();
            return context.ExternalSuppliers
                .Include(e => e.Address)
                .Include(e => e.Phones)
                .Include(e => e.EmailAddress)
                .FirstOrDefault(e => e.Id == Id);
        }
        #endregion
        #region Add
        /// <summary>
        /// Add new external supplier to database.
        /// </summary>
        /// <param name="externalSupplier">New external supplier.</param>
        public static void Add(ExternalSupplier externalSupplier)
        {
            if (externalSupplier != null)
            {
                context.ExternalSuppliers.Add(externalSupplier);
                context.SaveChanges();
            }
        }
        #endregion
        #region GetExternalSupplierPhones
        /// <summary>
        /// Retrieves external supplier phone list from database.
        /// </summary>
        /// <returns>External supplier phone list.</returns>
        public IList<ExternalSupplierPhone> GetExternalSupplierPhones()
        {
            context = new ApplicationDbContext();
            return context.ExternalSupplierPhones.Where(e => e.ExternalSupplier.Id == this.Id).ToList();
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edit current external supplier in database.
        /// </summary>
        /// <param name="externalSupplier">Updated properties values of external supplier.</param>
        public void Edit(ExternalSupplier externalSupplier)
        {
            if (externalSupplier != null)
            {
                context = new ApplicationDbContext();
                var externalSupplierDb = context.ExternalSuppliers
                    .Include(e => e.Address)
                    .Include(e => e.Phones)
                    .FirstOrDefault(e => e.Id == externalSupplier.Id);

                if (externalSupplierDb != null)
                {
                    externalSupplierDb.Name = externalSupplier.Name;
                    externalSupplierDb.Tin = externalSupplier.Tin;
                    externalSupplierDb.Address.Country = externalSupplier.Address.Country;
                    externalSupplierDb.Address.City = externalSupplier.Address.City;
                    externalSupplierDb.Address.PostCode = externalSupplier.Address.PostCode;
                    externalSupplierDb.Address.HouseNumber = externalSupplier.Address.HouseNumber;
                    externalSupplierDb.Address.ApartmentNumber = externalSupplier.Address.ApartmentNumber;
                    context.SaveChanges();
                }
            }
        }
        #endregion
        #region AddPhone
        /// <summary>
        /// Add new phone to external supplier phone list.
        /// </summary>
        /// <param name="externalSupplierPhone">New external supplier phone.</param>
        public void AddPhone(ExternalSupplierPhone externalSupplierPhone)
        {
            if (externalSupplierPhone != null)
            {
                var externalSupplier = context.ExternalSuppliers.Include(e => e.Phones).FirstOrDefault(e => e.Id == this.Id);

                var number = new ExternalSupplierPhone()
                {
                    PhoneNumber = externalSupplierPhone.PhoneNumber,
                    Note = externalSupplierPhone.Note,
                    ExternalSupplier = externalSupplier
                };

                context.ExternalSupplierPhones.Add(number);
                context.SaveChanges();
            }
        }
        #endregion
        #region AddEmail
        /// <summary>
        /// Edits the email address of the external supplier.
        /// </summary>
        /// <param name="emailAddress">New email address.</param>
        public void EditEmail(ExternalSupplierEmailAddress emailAddress)
        {
            if (emailAddress != null)
            {
                var externalDb = Get(this.Id);

                if (externalDb.EmailAddress != null)
                {
                    externalDb.EmailAddress.EmailAddress = emailAddress.EmailAddress;
                    externalDb.EmailAddress.Note = emailAddress.Note;
                }
                else
                {
                    context.ExternalSupplierEmailAddresses.Add(new ExternalSupplierEmailAddress()
                    {
                        ExternalSupplier = externalDb,
                        EmailAddress = emailAddress.EmailAddress,
                        Note = emailAddress.Note
                    });
                }

                context.SaveChanges();
            }
        }
        #endregion
    }
}
