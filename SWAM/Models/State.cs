using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model representing the current status of products in the warehouse.
    /// </summary>
    public class State
    {
        int _id;
        /// <summary>
        /// Product quantity avaiable to sale
        /// </summary>
        int _available;
         
        Warehouse.Warehouse _warehouse;
        IList<CustomerOrderPosition> _customerOrderPositions;
        IList<WarehouseOrderPosition> _warehouseOrderPositions;

        public int Id { get => _id; set => _id = value; }
        public int Available { get => _available; set => _available = value; }
        /// <summary>
        /// Product quantity booked for buyer
        /// </summary>
        public int Booked { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse.Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get => _warehouseOrderPositions; set => _warehouseOrderPositions = value; }

        //---------------

        private static readonly ApplicationDbContext _context = new ApplicationDbContext();

        private static ApplicationDbContext context
        {
            get
            {
                return _context;
            }
        }
        public static void EditState(State state)
        {
            if (state != null)
            {
                var dbState = context.States.FirstOrDefault(p => p.Id == state.Id);

                dbState.Quantity = state.Quantity;
                dbState.Available = state.Quantity - dbState.Booked;

                context.SaveChanges();
            }
        }
        public static List<State> AllStates() => context.States.Include("Product").Include("Warehouse").ToList();

        public static List<State> GetStatesFromWarehouse(int warehouseId)
            => context.States
                    .Include(s => s.Product)
                    .Include(s => s.Warehouse)
                    .Where(s => s.WarehouseId == warehouseId).ToList();
    }
}

