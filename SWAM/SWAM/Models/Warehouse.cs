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
        IList<User> _users;
        IList<CustomerOrder> _customerOrders;
        [Key]
        [ForeignKey("Address")]
        int _addressId;

        Address _address;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<User> Users { get => _users; set => _users = value; }
        public int AddressId { get => _addressId; set => _addressId = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
        public Address Address
        {
            get
            {
                //TODO: Catch exceptions - Warehouse - Address
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this._address = context.Addresses.FirstOrDefault(a => a.Id == this._addressId);
                };
                return this._address;
            }
        }

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
