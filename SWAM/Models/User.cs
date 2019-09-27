using System;
using System.Collections.Generic;
using System.Linq;
using SWAM.Enumerators;
using SWAM.Exceptions;
using System.Windows;
using SWAM.Strings;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the application user.
    /// </summary>
    public class User : Person
    {
        /// <summary>
        /// Password with salt and hashed.
        /// </summary>
        public byte[] Password { get; set; }
        /// <summary>
        /// Password salt key - genereted pseudorandom bytes.
        /// </summary>
        public byte[] PasswordSalt { get; set; }
        /// <summary>
        /// The type of permission the user has in the application.
        /// </summary>
        public UserType Permissions { get; set; }
        /// <summary>
        /// Current user account status.
        /// </summary>
        public StatusOfUserAccount StatusOfUserAccount { get; set; }
        /// <summary>
        /// Date of create account.
        /// </summary>
        public DateTime DateOfCreate { get; set; }
        /// <summary>
        /// Account lock expiration date.
        /// </summary>
        public DateTime? ExpiryDateOfTheBlockade { get; set; }
        /// <summary>
        /// Account expiration date.
        /// </summary>
        public DateTime? DateOfExpiryOfTheAccount { get; set; }
        /// <summary>
        /// All ids of warheouses where user have permition to  access.
        /// </summary>
        public IList<AccessUsersToWarehouses> Accesess { get; set; }
        /// <summary>
        /// All user phone numbers.
        /// </summary>
        public IList<Phone> Phones { get; set; }
        /// <summary>
        /// All user emails.
        /// </summary>
        public IList<EmailAddress> EmailAddresses { get; set; }
        /// <summary>
        /// Messages sent by the user.
        /// </summary>
        public IList<Message> Messages { get; set; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
        }

        #region TryLogIn
        /// <summary>
        /// Looking for a specific user in the database by name. When it finds it, it checks that the hashed password matches the one in the database. 
        /// To do this correctly, the user account must have a well-generated password salt stored in the database to perform cryptographic functions.
        /// </summary>
        /// <param name="name">Name of specific user.</param>
        /// <param name="password">Password to user account.</param>
        /// <returns>If  password is correct - User account with all informations from database. Else null.</returns>
        public static User TryLogIn(string name, string password)
        {
            throw new NotImplementedException();

            //if (_context.Users.FirstOrDefault(u => u.Name == name) is User userFinded)
            //{
            //    try
            //    {
            //        if (userFinded.PasswordSalt == null)
            //            throw new PasswordSaltNullException();
            //        //Creating hashed password from user input...
            //        var userPassword = Cryptography.CryptoService.ComputeHash(password, userFinded.PasswordSalt);

            //        //Comparing created hashed password from input with hashed password from database.
            //         if (userFinded.Password.SequenceEqual(userPassword))
            //         {
            //             return SWAM.MainWindow.SetLoggedInUser(_context.Users
            //                 .Include(u => u.Accesess)
            //                 .Include(u => u.Phones)
            //                 .Include(u => u.EmailAddresses)
            //                 .FirstOrDefault(u => u.Id == userFinded.Id));
            //         }
            //        throw new NotImplementedException();
            //    }
            //    catch (PasswordSaltNullException)
            //    {
            //        MessageBox.Show( $"{ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION} {ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION_TIP}", "Błąd",  MessageBoxButton.OK, MessageBoxImage.Information); }
            //}
            //return null;
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
                throw new NotImplementedException();
                //_context.Users.Add(user);
                //_context.SaveChanges();
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
            throw new NotImplementedException();
            //_context.Users.FirstOrDefault(u => u.Id == this.Id).Name = name;
            //_context.SaveChanges();
        }
        #endregion
        #region ChangePermissions
        /// <summary>
        /// Changing user permissions in database.
        /// </summary>
        /// <param name="userType">New perminssion.</param>
        public void ChangePermissions(UserType userType)
        {
            throw new NotImplementedException();
            //_context.Users.FirstOrDefault(u => u.Id == this.Id).Permissions = userType;
            //_context.SaveChanges();
        }
        #endregion
        #region ChangePassword
        /// <summary>
        /// Change user password in database.
        /// </summary>
        /// <param name="password">New password.</param>
        public void ChangePassword(byte[] password)
        {
            throw new NotImplementedException();
            //_context.Users.FirstOrDefault(u => u.Id == this.Id).Password = password;
            //_context.SaveChanges();
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
            throw new NotImplementedException();
            //User user = _context.Users.Find(userID);
            //       // .Include(a => a.Accesess)
            //      //  .Include(e => e.EmailAddresses)
            //      //  .Include(p => p.Phones);
            //return user;
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
                throw new NotImplementedException();
                //_context.Users.FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = dateTime;
                //_context.SaveChanges();
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
                throw new NotImplementedException();
                //_context.Users.FirstOrDefault(u => u.Id == this.Id).DateOfExpiryOfTheAccount = dateTime;
                //_context.SaveChanges();
            }
        }
        #endregion
        #region ChangeStatus
        /// <summary>
        /// Changing status of user account in database.
        /// </summary>
        /// <param name="statusOfUserAccount">New status of account.</param>
        public void ChangeStatus(StatusOfUserAccount statusOfUserAccount)
        {
            throw new NotImplementedException();
            //_context.Users.FirstOrDefault(u => u.Id == this.Id).StatusOfUserAccount = statusOfUserAccount;
            //_context.SaveChanges();
        }
        #endregion  
    }
}
