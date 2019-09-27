using System.Collections.Generic;
using System.Linq;


namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the warehouse.
    /// </summary>
    public class Warehouse
    {
        /// <summary>
        /// Identification number in the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Warehouse name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Warehouse height.
        /// </summary>
        public long Height { get; set; }
        /// <summary>
        /// Warehouse width.
        /// </summary>
        public long Width { get; set; }
        /// <summary>
        /// Warehouse lenght.
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// Net area available in the warehouse.
        /// </summary>
        public long SurfaceAreaNetto { get; set; }
        /// <summary>
        /// Gross area available in the warehouse.
        /// </summary>
        public long SurfaceAreaBrutton { get; set; }
        /// <summary>
        /// Maximum total weight of products in the warehouse.
        /// </summary>
        public long AcceptableWeight { get; set; }
        /// <summary>
        /// Warehouse address.
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// All permissions granted to users for this storage.
        /// </summary>
        public IList<AccessUsersToWarehouses> Accesses { get; set; }
        /// <summary>
        /// All customer orders associated with this warehouse.
        /// </summary>
        public IList<CustomerOrder> CustomerOrders { get; set; }
        /// <summary>
        /// All order requirements for this warehouse.
        /// </summary>
        public IList<WarehouseOrder> WarehouseOrders { get; set; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
        }


        #region Remove
        /// <summary>
        /// Remove user from database.
        /// </summary>
        public bool Remove()
        {
            //TODO: Make block try-catch
            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == this.Id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        #endregion


        #region GetAllWharehousesFromDb
        /// <summary>
        /// Get list of all Warehouses from database
        /// </summary>
        /// <returns>IList with all warehouses in database</returns>
        public static IList<Warehouse> GetAllWharehousesFromDb()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Warehouses.ToList();
            }
        }
        #endregion
    }
}
