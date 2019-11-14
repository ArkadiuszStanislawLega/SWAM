using SWAM.Models.Warehouse;
using SWAM.Strings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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

        private static ApplicationDbContext context = new ApplicationDbContext();
        private static ApplicationDbContext Context
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
        #region Get
        /// <summary>
        /// Resived externa supplier from database.
        /// </summary>
        /// <param name="Id">External supplier number Id in database.</param>
        /// <returns>External supplier from database.</returns>
        public static ExternalSupplier Get(int Id)
        {
            Context = new ApplicationDbContext();
            return Context.ExternalSuppliers
                .Include(e => e.Address)
                .Include(e => e.Phones)
                .Include(e => e.EmailAddress)
                .FirstOrDefault(e => e.Id == Id);
        }
        #endregion
        #region IsExternalSupplierInDatabase
        /// <summary>
        /// Checks if the name is already in the database.
        /// </summary>
        /// <param name="name">Name of the external supplier to be retrieved in the database.</param>
        /// <returns>True if name is already exist in database.</returns>
        public static bool IsExternalSupplierInDatabase(string name)
        {
            if(name != string.Empty)
            {
                if (Context.People.FirstOrDefault(e => e.Name == name) != null)
                    return true;
            }

            return false;
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
                Context.ExternalSuppliers.Add(externalSupplier);
                Context.SaveChanges();
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
            Context = new ApplicationDbContext();
            return Context.ExternalSupplierPhones.Where(e => e.ExternalSupplier.Id == this.Id).ToList();
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
                Context = new ApplicationDbContext();
                var externalSupplierDb = Context.ExternalSuppliers
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
                    Context.SaveChanges();
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
                var externalSupplier = Context.ExternalSuppliers.Include(e => e.Phones).FirstOrDefault(e => e.Id == this.Id);

                var number = new ExternalSupplierPhone()
                {
                    PhoneNumber = externalSupplierPhone.PhoneNumber,
                    Note = externalSupplierPhone.Note,
                    ExternalSupplier = externalSupplier
                };

                Context.ExternalSupplierPhones.Add(number);
                Context.SaveChanges();
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
                    Context.ExternalSupplierEmailAddresses.Add(new ExternalSupplierEmailAddress()
                    {
                        ExternalSupplier = externalDb,
                        EmailAddress = emailAddress.EmailAddress,
                        Note = emailAddress.Note
                    });
                }

                Context.SaveChanges();
            }
        }
        #endregion
    }
}
