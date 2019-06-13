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
        public int WarehouseId { get; set; }

        [Required]
        [ForeignKey("Administrator")]
        public int AdministratorId { get; set; }

        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual User Administrator { get; set; }

        public int Id { get => _id; set => _id = value; }
        public UserType TypeOfAccess { get => _typeOfAccess; set => _typeOfAccess = value; }
        public DateTime DateOfGrantingAccess { get => _dateOfGrantingAccess; set => _dateOfGrantingAccess = value; }
        public DateTime? DateOfExpiredAcces { get => _dateOfExpiredAccess; set => _dateOfExpiredAccess = value; }
    }
}