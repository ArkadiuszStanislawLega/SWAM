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

        public IList<ExternalSupplierPhone> Phones { get; set; }

        public IList<WarehouseOrder> WarehouseOrders { get; set; }


        private static ApplicationDbContext context = new ApplicationDbContext();

        public static void Add(ExternalSupplier externalSupplier)
        {
            if (externalSupplier != null)
            {
                context.ExternalSuppliers.Add(externalSupplier);
                context.SaveChanges();
            }
        }

        public IList<ExternalSupplierPhone> GetExternalSupplierPhones()
        {
            context = new ApplicationDbContext();
            return context.ExternalSupplierPhones.Where(e => e.ExternalSupplier.Id == this.Id).ToList();
        }

        public void AddPhone(ExternalSupplierPhone externalSupplierPhone)
        {
            if (externalSupplierPhone != null)
            {
                var number = new ExternalSupplierPhone()
                {
                    PhoneNumber = externalSupplierPhone.PhoneNumber,
                    Note = externalSupplierPhone.Note,
                    ExternalSupplier = this
                };

                context.ExternalSupplierPhones.Add(number);

                context.SaveChanges();
            }
        }


    }
}
