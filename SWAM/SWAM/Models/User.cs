using System;
using System.Collections.Generic;
using System.Linq;
using SWAM.Enumerators;
using System.Data.Entity;

namespace SWAM.Models
{
    public class User
    {
        int _id;
        string _name;
        string _password;
        UserType _permissions;
        StatusOfUserAccount _statusOfUserAccount;
        DateTime _dateOfCreate;
        DateTime? _dateOfExpiryOfTheAccount;
        DateTime? _expiryDateOfTheBlockade;
        /// <summary>
        /// All user phone numbers.
        /// </summary>
        IList<Phone> _phones;
        /// <summary>
        /// All user emails.
        /// </summary>
        IList<Email> _emails;
        /// <summary>
        /// All ids of warheouses where user have permition to  access.
        /// </summary>
        public IList<AccessUsersToWarehouses> Accesess { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }
        public DateTime DateOfCreate { get => _dateOfCreate; set => _dateOfCreate = value; }
        public DateTime? ExpiryDateOfTheBlockade { get => _expiryDateOfTheBlockade; set => _expiryDateOfTheBlockade = value; }
        public DateTime? DateOfExpiryOfTheAccount { get => _dateOfExpiryOfTheAccount; set => _dateOfExpiryOfTheAccount = value; }
        public StatusOfUserAccount StatusOfUserAccount { get => _statusOfUserAccount; set => _statusOfUserAccount = value; }
        public string Password { get => _password; set => _password = value; }
        public IList<Phone> Phones { get => _phones; set => _phones = value; }
        public IList<Email> Emails { get => _emails; set => _emails = value; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext context()
        {
            //TODO: Make all exceptions
          
                return DB_CONTEXT;
  
        }
        #region Remove
        /// <summary>
        /// Remove user from database.
        /// </summary>
        public void Remove()
        {
            //TODO: Poprawić model, trzeba zrobić kaskadowe usuwanie, z access to warehouses
            var user = context().Users.FirstOrDefault(u => u.Id == this.Id);
            context().Users.Remove(user);
            context().SaveChanges();
        }
        #endregion

        #region CreateNewUser
        /// <summary>
        /// Add new user to database.
        /// </summary>
        /// <param name="user"></param>
        public static void AddNewUser(User user)
        {
            if (user != null)
            {
                context().Users.Add(user);
                context().SaveChanges();
            }
        }
        #endregion
        #region ChangeName
        /// <summary>
        /// Changing name of the user in database.
        /// </summary>
        /// <param name="name">New name of user.</param>
        public void ChangeName(string name)
        {
            context().Users.FirstOrDefault(u => u.Id == this.Id).Name = name;
            context().SaveChanges();
        }
        #endregion
        #region ChangePermissions
        /// <summary>
        /// Changing user permissions in database.
        /// </summary>
        /// <param name="userType">New perminssion.</param>
        public void ChangePermissions(UserType userType)
        {
            context().Users.FirstOrDefault(u => u.Id == this.Id).Permissions = userType;
            context().SaveChanges();
        }
        #endregion
        #region ChangePassword
        /// <summary>
        /// Change user password in database.
        /// </summary>
        /// <param name="password">New password.</param>
        public void ChangePassword(string password)
        {
            context().Users.FirstOrDefault(u => u.Id == this.Id).Password = password;
            context().SaveChanges();
        }
        #endregion
        #region GetUser
        /// <summary>
        /// Find user in database.
        /// </summary>
        /// <param name="userID">User number id in database.</param>
        /// <returns>Sepcific User by Id included accesses, email and phones.</returns>
        public static User GetUser(int userID)
        {
                return context().Users
                    .Include(a => a.Accesess)
                    .Include(e => e.Emails)
                    .Include(p => p.Phones)
                    .FirstOrDefault(u => u.Id == userID);
        }
        #endregion
        #region ChangeExpiryDateOfTheBlockade
        /// <summary>
        /// Changing date of the blockade user account.
        /// </summary>
        /// <param name="dateTime">New date od blockade of user accout.</param>
        public void ChangeExpiryDateOfTheBlockade(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                context().Users.FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = dateTime;
                context().SaveChanges();
            }
        }
        #endregion
        #region ChangeDateOfExpiryOfTheAccount
        /// <summary>
        /// Changing date of the blockade user account.
        /// </summary>
        /// <param name="dateTime">New date of expiry user accout.</param>
        public void ChangeDateOfExpiryOfTheAccount(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                context().Users.FirstOrDefault(u => u.Id == this.Id).DateOfExpiryOfTheAccount = dateTime;
                context().SaveChanges();
            }
        }
        #endregion
    }
}
