using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using SWAM.Strings;

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

        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        private static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return dbContext;
                }
                catch (DbUpdateConcurrencyException e) 
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null; 
                }
                catch (DbUpdateException e) 
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e) 
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null; 
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => dbContext = value;
        }
        #region AccessUsersToWarehouses
        /// <summary>
        /// Gets user access to the warehouse from the database by the access ID number.
        /// </summary>
        /// <returns>Returns user specific access to the store.</returns>
        public AccessUsersToWarehouses Get()
        {
            Context = new ApplicationDbContext();
            return Context.AccessUsersToWarehouses
                .Include(a => a.Administrator)
                .Include(a => a.User)
                .Include(a => a.Warehouse)
                .FirstOrDefault(a => a.Id == this.Id);
        }
        #endregion
        #region GetUserAccesses
        /// <summary>
        /// Get list with specific user accesses to warehouses from database. 
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List with user accesses to warehouses</returns>
        public static IList<AccessUsersToWarehouses> GetUserAccesses(int userId)
        {
            Context = new ApplicationDbContext();
            try
            {
                if (userId > 0)
                    return Context.People
                        .OfType<User.User>()
                        .Include(u => u.Accesess.Select(a=>a.Warehouse.WarehouseAddress))
                        .FirstOrDefault(u => u.Id == userId)
                        .Accesess;
                else
                    return null;
            }
            //When the table of accesses in database is empty.
            catch (NullReferenceException)
            {
                return null;
            }
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
                var removeAccss = Context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == accessId);
                if (removeAccss != null)
                {
                    Context.AccessUsersToWarehouses.Remove(removeAccss);
                    Context.SaveChanges();
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
            Context = new ApplicationDbContext();

            if (accessUsersToWarehouses != null
                && accessUsersToWarehouses.User.Id > 0
                && accessUsersToWarehouses.Administrator.Id > 0
                && accessUsersToWarehouses.Warehouse.Id > 0)
            {
                var user = Context.People
                    .OfType<User.User>()
                    .FirstOrDefault(u => u.Id == accessUsersToWarehouses.User.Id);

                var administrator = Context.People
                    .OfType<User.User>()
                    .FirstOrDefault(u => u.Id == accessUsersToWarehouses.Administrator.Id);

                var warehouse = Context.Warehouses
                    .FirstOrDefault(w => w.Id == accessUsersToWarehouses.Warehouse.Id);

                AccessUsersToWarehouses access = new AccessUsersToWarehouses()
                {
                    TypeOfAccess = accessUsersToWarehouses.TypeOfAccess,
                    Administrator = administrator,
                    User = user,
                    Warehouse = warehouse,
                    DateOfGrantingAccess = DateTime.Now
                };

                if (user.Accesess != null)
                    user.Accesess.Add(access);
                else
                {
                    user.Accesess = new List<AccessUsersToWarehouses>
                    {
                        access
                    };
                }

                if (Context.SaveChanges() == 4)
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
            Context.AccessUsersToWarehouses
                .FirstOrDefault(a => a.Id == this.Id)
                .DateOfExpiredAcces = dateTime;

            Context.SaveChanges();
        }
        #endregion
        #region EditTypeOfAccess
        /// <summary>
        /// Edit type of access in database.
        /// </summary>
        /// <param name="userType">New type of access.</param>
        public void EditTypeOfAccess(UserType userType)
        {
            Context.AccessUsersToWarehouses.FirstOrDefault(a => a.Id == this.Id).TypeOfAccess = userType;
            Context.SaveChanges();
        }
        #endregion
    }
}