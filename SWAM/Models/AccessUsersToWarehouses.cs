using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWAM.Models
{
    /// <summary>
    /// User access to specific warehouses with specific type of permissions.
    /// </summary>
    public class AccessUsersToWarehouses
    {
        /// <summary>
        /// Number Id of access in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type of access in warehouse.
        /// </summary>
        public UserType TypeOfAccess { get; set; }
        /// <summary>
        /// Date of granted access.
        /// </summary>
        public DateTime DateOfGrantingAccess { get; set; }
        /// <summary>
        /// Date of expiring access.
        /// </summary>
        public DateTime? DateOfExpiredAcces { get; set; }
        /// <summary>
        /// User who is granted the rights.
        /// </summary>
        public virtual User.User User { get; set; }
        /// <summary>
        /// User who granted the permissions.
        /// </summary>
        public virtual User.User Administrator { get; set; }
        /// <summary>
        /// The warehouse where permissions are granted.
        /// </summary>
        public virtual Warehouse.Warehouse Warehouse { get; set; }

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
        public static IList<AccessUsersToWarehouses> GetUserAccesses(int userId)
        {
            var user = context().People.OfType<User.User>().FirstOrDefault(u => u.Id == userId);
            return user.Accesess;
        }
        #endregion
        #region RemoveAccess
        /// <summary>
        /// Remove specific access from database.
        /// </summary>
        /// <param name="accessId">Number id from database.</param>
        /// <returns>True - access has been removed, false - access Id is lower than 0.</returns>
        public static bool RemoveAccess(int accessId)
        {
            if (accessId > 0)
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
                if(context().SaveChanges() == 1)
                    return true;
            }

            return false;
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