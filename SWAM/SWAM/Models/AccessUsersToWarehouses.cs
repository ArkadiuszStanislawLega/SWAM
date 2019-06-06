using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;

namespace SWAM.Models
{
    public class AccessUsersToWarehouses
    {
        int _id;
        [Key]
        [ForeignKey("User")]
        int _userId;
        [Key]
        [ForeignKey("Warehouse")]
        int _warehouseId;
        [Key]
        [ForeignKey("User")]
        int _whoGaveAccessUserId;
        UserType _typeOfAccess;
        DateTime _dateOfGrantingAccess;
        DateTime? _dateOfExpiredAccess;
        
        public int Id { get => _id; set => _id = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int WarehouseId { get => _warehouseId; set => _warehouseId = value; }
        public int WhoGaveAccessUserId { get => _whoGaveAccessUserId; set => _whoGaveAccessUserId = value; }
        public UserType TypeOfAccess { get => _typeOfAccess; set => _typeOfAccess = value; }
        public DateTime DateOfGrantingAccess { get => _dateOfGrantingAccess; set => _dateOfGrantingAccess = value; }
        public DateTime? DateOfExpiredAcces { get => _dateOfExpiredAccess; set => _dateOfExpiredAccess = value; }

        public string AdministratorNameWhoGaveGrant
        {
            get
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    return context.Users.FirstOrDefault(u => u.Id == this._whoGaveAccessUserId).Name;
                }
            }
        }

        /// <summary>
        /// Warehouse to which the rights apply.
        /// </summary>
        public Warehouse CurrentWarehouse
        {
            get
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    return context.Warehouses.FirstOrDefault(w => w.Id == this._warehouseId);
                }
            }
        }
        
        /// <summary>
        /// List of current user access to warehouses list.
        /// </summary>
        /// <returns>List of user access list.</returns>
        public List<AccessUsersToWarehouses> UserListOfAccess()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return  context.AccessUsersToWarehouses
                        .SqlQuery("Select * from AccessUsersToWarehouses where UserId=@userId", new SqlParameter("@userId", this._userId))
                        .ToList<AccessUsersToWarehouses>();
            }
        }
    }
}