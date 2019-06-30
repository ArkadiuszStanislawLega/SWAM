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
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Warehouse))]
        public int WarehouseId { get; set; }

        [Required]
        [ForeignKey(nameof(Administrator))]
        public int AdministratorId { get; set; }

        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual User Administrator { get; set; }

        public int Id { get => _id; set => _id = value; }
        public UserType TypeOfAccess { get => _typeOfAccess; set => _typeOfAccess = value; }
        public DateTime DateOfGrantingAccess { get => _dateOfGrantingAccess; set => _dateOfGrantingAccess = value; }
        public DateTime? DateOfExpiredAcces { get => _dateOfExpiredAccess; set => _dateOfExpiredAccess = value; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext context()
        {
            //TODO: Make all exceptions

            return DB_CONTEXT;

        }
        #region GetUserAccesses
        /// <summary>
        /// Get list with specific user accesses to warehouses from database. 
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List with user accesses to warehouses</returns>
        public static IEnumerable<AccessUsersToWarehouses> GetUserAccesses(int userId) => context().AccessUsersToWarehouses.ToList().Where(u => u.UserId == userId);
        #endregion
        #region RemoveAccess
        /// <summary>
        /// Remove specific access from database.
        /// </summary>
        /// <param name="accessId">Number id from database.</param>
        /// <returns>True - access has been removed, false - access Id is lower than 0.</returns>
        public static bool RemoveAccess(int accessId)
        {
            if (accessId > 0 )
            {
                var removeAccss = context().AccessUsersToWarehouses.FirstOrDefault(a => a.Id == accessId);
                if (removeAccss != null)
                {
                    context().AccessUsersToWarehouses.Remove(removeAccss);
                    context().SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
        #endregion
        #region AddNewAccess
        /// <summary>
        /// Adding new access to database.
        /// </summary>
        /// <param name="accessUsersToWarehouses">New user access to warehouse.</param>
        /// <returns>True - access has been added, false - access is null.</returns>
        public static bool AddNewAccess(AccessUsersToWarehouses accessUsersToWarehouses)
        {
            if (accessUsersToWarehouses != null)
            {
                context().AccessUsersToWarehouses.Add(accessUsersToWarehouses);
                context().SaveChanges();
                return true;
            }
            else return false;
        }
        #endregion

        #region EditExpiredAccess
        /// <summary>
        /// Edit date of expired user access in database.
        /// </summary>
        /// <param name="userType">New type of access.</param>
        public void EditExpiredAccess(DateTime? dateTime)
        {
            context().AccessUsersToWarehouses.FirstOrDefault(a => a.Id == this.Id).DateOfExpiredAcces = dateTime;
            context().SaveChanges();
        }
        #endregion
        #region EditTypeOfAccess
        /// <summary>
        /// Edit type of access in database.
        /// </summary>
        /// <param name="userType">New type of access.</param>
        public void EditTypeOfAccess(UserType userType)
        {
            context().AccessUsersToWarehouses.FirstOrDefault(a => a.Id == this.Id).TypeOfAccess = userType;
            context().SaveChanges();
        }
        #endregion
    }
}