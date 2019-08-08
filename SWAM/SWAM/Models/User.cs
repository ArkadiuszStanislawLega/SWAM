using System;
using System.Collections.Generic;
using System.Linq;
using SWAM.Enumerators;
using System.Data.Entity;
using SWAM.Exceptions;
using System.Windows;

namespace SWAM.Models
{
    public class User
    {
        int _id;
        string _name;
        /// <summary>
        /// Password with salt and hashed.
        /// </summary>
        byte[] _password;
        /// <summary>
        /// Password salt key - genereted pseudorandom bytes.
        /// </summary>
        byte[] _passwordSalt;
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

        public IList<Message> Messages { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }
        public DateTime DateOfCreate { get => _dateOfCreate; set => _dateOfCreate = value; }
        public DateTime? ExpiryDateOfTheBlockade { get => _expiryDateOfTheBlockade; set => _expiryDateOfTheBlockade = value; }
        public DateTime? DateOfExpiryOfTheAccount { get => _dateOfExpiryOfTheAccount; set => _dateOfExpiryOfTheAccount = value; }
        public StatusOfUserAccount StatusOfUserAccount { get => _statusOfUserAccount; set => _statusOfUserAccount = value; }
        public byte[] Password { get => _password; set => _password = value; }
        public IList<Phone> Phones { get => _phones; set => _phones = value; }
        public IList<Email> Emails { get => _emails; set => _emails = value; }
        public byte[] PasswordSalt { get => _passwordSalt; set => _passwordSalt = value; }

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
            if (_context.Users.FirstOrDefault(u => u.Name == name) is User userFinded)
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
                        SWAM.MainWindow.LoggedInUser = null;
                        SWAM.MainWindow.LoggedInUser = _context.Users
                            .Include(u => u.Accesess)
                            .Include(u => u.Phones)
                            .Include(u => u.Emails)
                            .FirstOrDefault(u => u.Id == userFinded.Id);

                        return SWAM.MainWindow.LoggedInUser;
                    }
                }
                catch (PasswordSaltNullException)
                {
                    MessageBox.Show( $"{ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION} {ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION_TIP}", "Błąd",  MessageBoxButton.OK, MessageBoxImage.Information); }
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
                _context.Users.Add(user);
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
            _context.Users.FirstOrDefault(u => u.Id == this.Id).Name = name;
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
            _context.Users.FirstOrDefault(u => u.Id == this.Id).Permissions = userType;
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
            _context.Users.FirstOrDefault(u => u.Id == this.Id).Password = password;
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
            return _context.Users
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
                _context.Users.FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = dateTime;
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
                _context.Users.FirstOrDefault(u => u.Id == this.Id).DateOfExpiryOfTheAccount = dateTime;
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
            _context.Users.FirstOrDefault(u => u.Id == this.Id).StatusOfUserAccount = statusOfUserAccount;
            _context.SaveChanges();
        }
        #endregion  
    }
}
