using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Warehouse
    {
        int _id;
        string _name;
        long _height;
        long _width;
        long _surfaceAreaNetto;
        long _surfaceAreaBrutton;
        long _acceptableWeight;
        IList<AccessUsersToWarehouses> _accesses;
        IList<CustomerOrder> _customerOrders;

        [Required]
        [ForeignKey("Address")]
        public int _addressId { get; set; }
        public virtual Address Address { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public long Height { get => _height; set => _height = value; }
        public long Width { get => _width; set => _width = value; }
        public long SurfaceAreaNetto { get => _surfaceAreaNetto; set => _surfaceAreaNetto = value; }
        public long SurfaceAreaBrutton { get => _surfaceAreaBrutton; set => _surfaceAreaBrutton = value; }
        public long AcceptableWeight { get => _acceptableWeight; set => _acceptableWeight = value; }

        public int AddressId { get => _addressId; set => _addressId = value; }
        public IList<AccessUsersToWarehouses> Accesses { get => _accesses; set => _accesses = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }


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
