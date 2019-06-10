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
        UserType _typeOfAccess;
        DateTime _dateOfGrantingAccess;
        DateTime? _dateOfExpiredAccess;

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Warehouse")]
        public  int WarehouseId { get; set; }

        [Required]
        [ForeignKey("Administrator")]
        public int AdministratorId;

        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual User Administrator { get; set; }


        public int Id { get => _id; set => _id = value; }
        public UserType TypeOfAccess { get => _typeOfAccess; set => _typeOfAccess = value; }
        public DateTime DateOfGrantingAccess { get => _dateOfGrantingAccess; set => _dateOfGrantingAccess = value; }
        public DateTime? DateOfExpiredAcces { get => _dateOfExpiredAccess; set => _dateOfExpiredAccess = value; }

        
        /// <summary>
        /// List of current user access to warehouses list.
        /// </summary>
        /// <returns>List of user access list.</returns>
        public List<AccessUsersToWarehouses> UserListOfAccess()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return  context.AccessUsersToWarehouses
                        .SqlQuery("Select * from AccessUsersToWarehouses where UserId=@userId", new SqlParameter("@userId", this.UserId))
                        .ToList<AccessUsersToWarehouses>();
            }
        }
    }
}