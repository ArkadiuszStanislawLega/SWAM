using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int available;
        /// <summary>
        /// Product quantity booked for buyer
        /// </summary>
        int booked;
        Warehouse.Warehouse _warehouse;
        IList<CustomerOrderPosition> _customerOrderPositions;
        IList<WarehouseOrderPosition> _warehouseOrderPositions;

        public int Id { get => _id; set => _id = value; }
        public int Available { get => available; set => available = value; }
        public int Booked { get => booked; set => booked = value; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get => _warehouseOrderPositions; set => _warehouseOrderPositions = value; }
        public Warehouse.Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
    }
}
