using System;
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

        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return dbContext;
            }
            set => dbContext = value;
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
            => 
                    _context.People.OfType<User>()
                        .Include(u => u.Accesess)
                        .Include(u => u.EmailAddresses)
                        .Include(u => u.Phones)
                        .First(u => u.Id == userID);
        
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

        #region AddEmail
        /// <summary>
        /// Add new email to database.
        /// </summary>
        /// <param name="email">New addres email.</param>
        public void AddUserAddressEmail(UserEmailAddress email)
        {
            if (email != null)
            {
                //TODO: try - catch block is needed ... when excetion will be catch than send false.
                _context.People.OfType<User>()
                    .Include(u => u.EmailAddresses)
                    .FirstOrDefault(u => u.Id == this.Id)
                    .EmailAddresses.Add(email);
                _context.SaveChanges();
            }
        }
        #endregion

        #region AddNewPhone
        /// <summary>
        /// Adding new Phone to database.
        /// </summary>
        /// <param name="phone">New added phone.</param>
        public bool AddNewPhone(UserPhone phone)
        {
            //TODO: try - catch block is needed ... when excetion will be catch than send false.
            if (phone != null)
            {
                _context = new ApplicationDbContext();

                var user = _context.People.OfType<User>()
                    .Include(u => u.Phones)
                    .FirstOrDefault(u => u.Id == this.Id);

                if (user != null)
                {
                    var userPhone = new UserPhone()
                    {
                        User = user,
                        PhoneNumber = phone.PhoneNumber,
                        Note = phone.Note
                    };

                    user.Phones.Add(userPhone);

                    if (_context.SaveChanges() == 2)
                        return true;
                }
            }

            return false;
        }
        #endregion
        #region UpdatePhoneNumber
        /// <summary>
        /// Update current phone number.
        /// </summary>
        /// <param name="newPhoneNumber">New phone/edited number.</param>
        public void UpdatePhoneNumber(UserPhone userPhone, string newPhoneNumber)
        {
            if (userPhone != null)
            {
                _context.People
                            .OfType<User>()
                            .Include(u => u.Phones)
                            .First(u => u.Id == this.Id)
                            .Phones.First(p => p.Id == userPhone.Id)
                            .PhoneNumber = newPhoneNumber;
                _context.SaveChanges();
            }
        }
        #endregion
        #region UpdatePhoneNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        public void UpdatePhoneNote(UserPhone userPhone, string newNote)
        {
            if (userPhone != null)
            {
                _context.People
                         .OfType<User>()
                         .Include(u => u.Phones)
                         .First(u => u.Id == this.Id)
                         .Phones.First(p => p.Id == userPhone.Id)
                         .PhoneNumber = newNote;
                _context.SaveChanges();
            }
        }
        #endregion
        #region DeletePhone
        /// <summary>
        /// Delete from database current number.
        /// </summary>
        /// <param name="userPhone">Phone to remove.</param>
        /// <returns>True if phone is correctly removed from database.</returns>
        public bool DeletePhone(UserPhone userPhone)
        {
            if (userPhone != null)
            {
                _context = new ApplicationDbContext();

                var phones = _context.Phones.FirstOrDefault(u => u.Id == userPhone.Id);

                if (phones != null)
                {
                    _context.Phones.Remove(phones);
                    //Two rows are affected in database.
                    //One in Phone table and one in UserPhone table.
                    if (_context.SaveChanges() == 2)
                        return true;
                }
            }

            return false;
        }
        #endregion
        #region GetUserPhones
        /// <summary>
        /// Make list with phones of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with phones.</returns>
        public IList<UserPhone> GetUserPhones()
        {
            _context = new ApplicationDbContext();
            return _context.People
                        .OfType<User>()
                        .Include(u => u.Phones)
                        .First(u => u.Id == this.Id).Phones;
        }
        #endregion

        #region ChangeEmailAddress
        /// <summary>
        /// Change specific email address of user to new one.
        /// </summary>
        /// <param name="editedEmailAddress">Edited email addres.</param>
        /// <param name="emailAddress">New email address.</param>
        /// <returns>True if the email address could be changed.</returns>
        public bool ChangeEmailAddress (UserEmailAddress editedEmailAddress, string emailAddress) 
        {
            if (editedEmailAddress != null)
            {
                User dbEmailOwner = _context.People.OfType<User>().First(u => u.Id == editedEmailAddress.User.Id);
                if (dbEmailOwner != null)
                {
                    if (dbEmailOwner.EmailAddresses != null)
                    {
                        dbEmailOwner.EmailAddresses.First(e => e.Id == editedEmailAddress.Id).AddressEmail = emailAddress;
                        if (_context.SaveChanges() == 1)
                            return true;
                    }
                }
            }

            return false;
        }
        #endregion
        #region GetEmailAddress
        /// <summary>
        /// Retrieves the user's email address from the database after the address Id number.
        /// </summary>
        /// <param name="emailAddressId">Id Email Address.</param>
        /// <returns>Specific user email address from database.</returns>
        public UserEmailAddress GetEmailAddress(int emailAddressId)
        {
            _context = new ApplicationDbContext();
            return _context.People.OfType<User>()
               .Include(u => u.EmailAddresses)
               .First(u => u.Id == this.Id)
               .EmailAddresses.First(e => e.Id == emailAddressId);
        }
        #endregion
        #region GetUserEmails
        /// <summary>
        /// Make list with email addresses of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with email addresses.</returns>
        public IList<UserEmailAddress> GetUserEmails()
        {
            User user = _context.People
                        .OfType<User>()
                        .Include(u => u.EmailAddresses)
                        .First(u => u.Id == this.Id);
            return user.EmailAddresses;
        }
        #endregion

        #region AllUsersList
        /// <summary>
        /// Gets the complete list of users from the database.
        /// </summary>
        /// <returns>Full list of users from the database.</returns>
        public static IList<User> AllUsersList() => _context
            .People.OfType<User>()
            .Include(u => u.Phones)
            .Include(u => u.Accesess)
            .Include(u => u.EmailAddresses)
            .ToList();
        #endregion
    }
}
