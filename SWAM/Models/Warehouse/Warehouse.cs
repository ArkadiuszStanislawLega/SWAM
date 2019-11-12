using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models.Warehouse
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
        /// Maximum total weight of products in the warehouse.
        /// </summary>
        public long AcceptableWeight { get; set; }
        public int WarehouseAddressId { get; set; }
        /// <summary>
        /// Warehouse address.
        /// </summary>
        public WarehouseAddress WarehouseAddress { get; set; }
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

        private static ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
            set => DB_CONTEXT = value;
        }

        #region IsWarehouseNameExist
        /// <summary>
        /// Checks the database to see if the warehouse name already exists.
        /// </summary>
        /// <param name="name">Name to check.</param>
        /// <returns>True if name already exists in database.</returns>
        public static bool IsWarehouseNameExist(string name)
        {
            if (name != string.Empty)
            {
                _context = new ApplicationDbContext(); 
                if (_context.Warehouses.FirstOrDefault(w => w.Name == name) != null)
                    return true;
            }

            return false;
        }
        #endregion
        #region IsAdd
        /// <summary>
        /// Adding new warehouse to database.
        /// </summary>
        /// <param name="warehouse">New warehouse.</param>
        /// <returns>True if the database addition was successful.</returns>
        public static bool IsAdd(Warehouse warehouse)
        {
            if (warehouse != null)
            {
                _context.Warehouses.Add(warehouse);

                if (_context.SaveChanges() == 3)
                    return true;
            }

            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the magazine from the database by ID number.
        /// </summary>
        /// <param name="id">Number Id of warehouse in database.</param>
        /// <returns>Returns Specific warehouse from database or null if warehouse doesn't exist.</returns>
        public Warehouse Get(int id)
        {
            if (id > 0)
            {
                _context = new ApplicationDbContext();
                return _context.Warehouses.FirstOrDefault(w => w.Id == id);
            }

            return null;
        }
        #endregion
        #region Edit
        /// <summary>
        /// Edits warehouse properties.
        /// </summary>
        /// <param name="warehouse">The warehouse to be edited.</param>
        public void Edit(Warehouse warehouse)
        {
            if(warehouse != null && warehouse.WarehouseAddress != null)
            {
                var warehouseDb = _context.Warehouses.Include(w => w.WarehouseAddress).FirstOrDefault(w => w.Id == warehouse.Id);

                warehouseDb.Name = warehouse.Name;
                warehouseDb.Height = warehouse.Height;
                warehouseDb.Width = warehouse.Width;
                warehouseDb.Length = warehouse.Length;
                warehouseDb.AcceptableWeight = warehouse.AcceptableWeight;
                warehouseDb.WarehouseAddress = warehouse.WarehouseAddress;

                _context.SaveChanges();
            }
        }
        #endregion
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
        #region GetAllWharehouses
        /// <summary>
        /// Get list of all Warehouses from database
        /// </summary>
        /// <returns>IList with all warehouses in database</returns>
        public static IList<Warehouse> GetAllWarehouses()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Warehouses.ToList();
            }
        }
        #endregion

        #region GetAllWarehousesIncludingFullAccesess
        /// <summary>
        /// Get list of all Warehouses from database including accessess and users.
        /// </summary>
        /// <returns>IList with all warehouses in database including accessess and users</returns>
        public static IList<Warehouse> GetAllWarehousesIncludingFullAccesess()
        {
            _context = new ApplicationDbContext();
            IList<Warehouse> dbWarehouses;

            dbWarehouses = _context.Warehouses
                .Include(a => a.WarehouseAddress)
                .Include(co => co.CustomerOrders)
                .Include(u => u.Accesses)
                .ToList();

            //TODO: Make this more pro!!
            foreach (Warehouse w in dbWarehouses)
            {
                for (int i = 0; i < w.Accesses.Count; i++)
                {
                    int id = w.Accesses[i].Id;
                    w.Accesses[i] = _context.AccessUsersToWarehouses
                      .Include(au => au.User)
                      .Include(au => au.Administrator)
                      .Include(au => au.Warehouse)
                      .FirstOrDefault(au => au.Id == id);
                }
            }
            return dbWarehouses;
        }
        #endregion

    }
}
