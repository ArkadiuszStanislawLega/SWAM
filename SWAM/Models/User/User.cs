﻿using System;
using System.Collections.Generic;
using System.Linq;
using SWAM.Enumerators;
using SWAM.Exceptions;
using System.Windows;
using SWAM.Strings;
using System.Data.Entity;

namespace SWAM.Models.User
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
        /// <summary>
        /// All user emails.
        /// </summary>
        /// <summary>
        /// Messages sent by the user.
        /// </summary>
        public IList<Message> Messages { get; set; }
        /// <summary>
        /// List with email addresses.
        /// </summary>
        public IList<UserEmailAddress> EmailAddresses { get; set; }
        /// <summary>
        /// List of users phones.
        /// </summary>
        public IList<UserPhone> Phones { get; set; }

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
            if (_context.People.FirstOrDefault(u => u.Name == name) is User userFinded)
            {
                try
                {
                    if (userFinded.PasswordSalt == null)
                        throw new PasswordSaltNullException();
                    //Creating hashed password from user input...
                    var userPassword = Cryptography.CryptoService.ComputeHash(password, userFinded.PasswordSalt);

                    //Comparing created hashed password from input with hashed password from database.
                    if (userFinded.Password.SequenceEqual(userPassword))
                    {
                        return SWAM.MainWindow.SetLoggedInUser(_context.People.OfType<User>()
                            .Include(u => u.Accesess)
                            .Include(u => u.Phones)
                            .Include(u => u.EmailAddresses)
                            .FirstOrDefault(u => u.Id == userFinded.Id));
                    }
                    throw new NotImplementedException();
                }
                catch (PasswordSaltNullException)
                {
                    MessageBox.Show($"{ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION} {ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION_TIP}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return null;
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
                _context.People.Add(user);
                _context.SaveChanges();
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
            _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).Name = name;
            _context.SaveChanges();
        }
        #endregion
        #region ChangePermissions
        /// <summary>
        /// Changing user permissions in database.
        /// </summary>
        /// <param name="userType">New perminssion.</param>
        public void ChangePermissions(UserType userType)
        {
            _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).Permissions = userType;
            _context.SaveChanges();
        }
        #endregion
        #region ChangePassword
        /// <summary>
        /// Change user password in database.
        /// </summary>
        /// <param name="password">New password.</param>
        public void ChangePassword(byte[] password)
        {
            _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).Password = password;
            _context.SaveChanges();
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

            //User user = _context.People.OfType<User>().FirstOrDefault(u => u.Id == userID);
                                     //.Include(a => a.Accesess)
                                     //.Include(e => e.EmailAddresses)
                                     //.Include(p => p.Phones);
            return _context.People.OfType<User>().FirstOrDefault(u => u.Id == userID);
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
                _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = dateTime;
                _context.SaveChanges();
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
                _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).DateOfExpiryOfTheAccount = dateTime;
                _context.SaveChanges();
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
            _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).StatusOfUserAccount = statusOfUserAccount;
            _context.SaveChanges();
        }
        #endregion  
    }
}