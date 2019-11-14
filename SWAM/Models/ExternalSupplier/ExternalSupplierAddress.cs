using SWAM.Strings;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace SWAM.Models.ExternalSupplier
{
    [Table("ExternalSuppliersAddresses")]
    public class ExternalSupplierAddress 
    {
        /// <summary>
        /// Number Id in database.
        /// </summary>
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }
        public ExternalSupplier ExternalSupplier { get; set; }

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

        #region Add
        /// <summary>
        /// Add external supplier address to datbase.
        /// </summary>
        /// <param name="externalSupplierAddress">New external supplier address.</param>
        public static void Add(ExternalSupplierAddress externalSupplierAddress)
        {
            if(externalSupplierAddress != null)
            {
                Context.ExternalSupplierAddresses.Add(externalSupplierAddress);
                Context.SaveChanges();
            }
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edit external supplier address in database.
        /// </summary>
        /// <param name="externalSupplierAddress">New properties values in database.</param>
        public void Edit(ExternalSupplierAddress externalSupplierAddress)
        {
            if (externalSupplierAddress != null)
            {
                Context = new ApplicationDbContext();
                var externalAddressDb = Context.ExternalSupplierAddresses
                    .Include(e => e.ExternalSupplier)
                    .FirstOrDefault(e => e.Id == this.Id);

                if (externalAddressDb != null)
                {
                    externalAddressDb.Country = externalSupplierAddress.Country;
                    externalAddressDb.PostCode = externalSupplierAddress.PostCode;
                    externalAddressDb.Street = externalSupplierAddress.Street;
                    externalAddressDb.City = externalSupplierAddress.City;
                    externalAddressDb.HouseNumber = externalSupplierAddress.HouseNumber;
                    externalAddressDb.ApartmentNumber = externalSupplierAddress.ApartmentNumber;

                    Context.SaveChanges();
                }
            }
        }
        #endregion
    }
}
