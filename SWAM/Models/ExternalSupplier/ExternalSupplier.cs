
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


        private static ApplicationDbContext context = new ApplicationDbContext();

        public static void Add(ExternalSupplier externalSupplier)
        {
            if (externalSupplier != null)
            {
                context.ExternalSuppliers.Add(externalSupplier);
                context.SaveChanges();
            }

        }
    }
}
