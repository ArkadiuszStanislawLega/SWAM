using System;
using System.Collections.Generic;
using System.Linq;
using SWAM.Enumerators;
using SWAM.Exceptions;
using System.Windows;
using SWAM.Strings;
using System.Data.Entity;
using System.Text.RegularExpressions;
using SWAM.Models.Warehouse;

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
        /// <summary>
        /// Warehouse orders accepted by the user.
        /// </summary>
        public IList<WarehouseOrder> WarehouseOrders { get; set; }

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

        #region IsValidPassword
        /// <summary>
        /// Checking the password in terms of password length and characters used in the password.
        /// </summary>
        /// <param name="pasword">Password to be checked.</param>
        /// <returns>True if the password meets all the requirements.</returns>
        public static bool IsValidPassword(string pasword)
        {
            var input = pasword;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if (hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input))
                return true;
            else
                return false;
        }
        #endregion

        #region TryLogIn
        /// <summary>
        /// Looking for a specific user in the database by name. When it finds it, it checks that the hashed password matches the one in the database. 
        /// To do this correctly, the user account must have a well-generated password salt stored in the database to perform cryptographic functions.
        /// </summary>
        /// <param name="name">Name of specific user.</param>
        /// <param name="password">Password to user account.</param>
        /// <returns>If  password is correct - User account with all informations from database. Else null.</returns>
        public static bool TryLogIn(string name, string password)
        {
            //Getting user password and password salt from database 
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
                        //Checking account status to see if it's blocked
                        if (userFinded.StatusOfUserAccount == StatusOfUserAccount.Blocked)
                        {
                            //Checking if the blockade date has passed...
                            if (userFinded.ExpiryDateOfTheBlockade <= DateTime.Now)
                            {
                                //If it's expired, it will unblock the account and clear date of blockade.
                                userFinded.ChangeStatus(StatusOfUserAccount.Active);
                                userFinded.ChangeExpiryDateOfTheBlockade(null);

                                MainWindow.Instance.InformationForUser($"Czas blokady konta minął, witamy ponownie.", true);
                            }
                            else
                            {
                                //Set information to user about blockade.
                                //If the blockade expiration date is set, count the number of days and hours that must elapse by the end of the blockade.
                                if (userFinded.ExpiryDateOfTheBlockade != null)
                                {
                                    var timeOfBlockadeLeft = userFinded.ExpiryDateOfTheBlockade - DateTime.Now;
                                    var stringBlockadeDay = timeOfBlockadeLeft.Value.Days == 1 ? "dzień" : "dni";

                                    MainWindow.Instance.InformationForUser($"Twoje konto jest zablokowane, skontaktuj się z administratorem. Do końca blokady pozostało: {timeOfBlockadeLeft.Value.Days} {stringBlockadeDay}, oraz {timeOfBlockadeLeft.Value.Hours}:{timeOfBlockadeLeft.Value.Minutes}:{timeOfBlockadeLeft.Value.Seconds}", true);
                                }
                                //If time is not seated, inform about pernament block of account.
                                else
                                {
                                    MainWindow.Instance.InformationForUser($"Twoje konto jest pernamentnie zablokowane, skontaktuj się z administratorem.", true);
                                }

                                return false;
                            }
                        }

                        //Checking if the account expiry time has not been exceeded.
                        if (userFinded.DateOfExpiryOfTheAccount <= DateTime.Now)
                        {
                            MainWindow.Instance.InformationForUser($"Upłynął czas aktywności konta, skontaktuj się z administratorem.", true);
                            return false;
                        }

                        #region***************If the login attempt was successful***************
                        var timeLeft = userFinded.DateOfExpiryOfTheAccount - DateTime.Now;
                        var stringDay = timeLeft.Value.Days == 1 ? "dzień" : "dni";

                        //Geting profile of user from database.
                        SWAM.MainWindow.SetLoggedInUser(_context.People.OfType<User>()
                            .Include(u => u.Accesess)
                            .Include(u => u.Phones)
                            .Include(u => u.EmailAddresses)
                            .FirstOrDefault(u => u.Id == userFinded.Id));
                        //Make bar with navigation buttons visible.
                        MainWindow.Instance.VisibleMode = Visibility.Visible;
                        //Refresh navigation buttons.
                        MainWindow.Instance.RefreshNavigationButtons();
                        //Message to user about login in to the system.
                        MainWindow.Instance.InformationForUser($"Witaj w systemie {userFinded.Name}. Do wygaśnięcia konta pozostało: {timeLeft.Value.Days} {stringDay}, oraz {timeLeft.Value.Hours}:{timeLeft.Value.Minutes}:{timeLeft.Value.Seconds}");
                        return true;
                        #endregion
                    }
                    else
                        //Password doesn't match.
                        MainWindow.Instance.InformationForUser($"{ErrorMesages.BAD_LOGIN_OR_PASSWORD}", true); 
                }
                catch (PasswordSaltNullException)
                {
                    MessageBox.Show($"{ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION} {ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION_TIP}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                //There is no such login in the database.
                MainWindow.Instance.InformationForUser($"{ErrorMesages.BAD_LOGIN_OR_PASSWORD}", true); 

            return false;
        }
        #endregion

        #region IsAdd
        /// <summary>
        /// Add new user to database.
        /// </summary>
        /// <param name="user"></param>
        public bool IsAdd(User user)
        {
            if (user != null)
            {
                _context.People.Add(user);
                if (_context.People.FirstOrDefault(c => c.Name == user.Name) == null)
                {
                    var number = _context.SaveChanges();
                    if (number == 1)
                        return true;
                }
            }
            return false;
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

                _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = dateTime;
                _context.SaveChanges();
            
            //else
            //{
            //    _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).DateOfExpiryOfTheAccount = DateTime.Parse("1/1/0001");
            //    _context.SaveChanges();
            //}
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
            if(statusOfUserAccount == StatusOfUserAccount.Active)
                _context.People.OfType<User>().FirstOrDefault(u => u.Id == this.Id).ExpiryDateOfTheBlockade = null;
            _context.SaveChanges();
        }
        #endregion

        #region AddPhone
        /// <summary>
        /// Adding new Phone to database.
        /// </summary>
        /// <param name="userPhone">New added phone.</param>
        public bool AddPhone(UserPhone userPhone)
        {
            if (userPhone != null)
            {
                _context = new ApplicationDbContext();

                var user = _context.People.OfType<User>()
                    .Include(u => u.Phones)
                    .FirstOrDefault(u => u.Id == this.Id);

                if (user != null)
                {
                    var dbUserPhone = new UserPhone()
                    {
                        User = user,
                        PhoneNumber = userPhone.PhoneNumber,
                        Note = userPhone.Note
                    };

                    user.Phones.Add(dbUserPhone);

                    if (_context.SaveChanges() == 2)
                        return true;
                }
            }

            return false;
        }
        #endregion
        #region GetPhones
        /// <summary>
        /// Make list with phones of specific user.
        /// </summary>
        /// <returns>List with phones.</returns>
        public IList<UserPhone> GetPhones()
        {
            _context = new ApplicationDbContext();
            return _context.People
                        .OfType<User>()
                        .Include(u => u.Phones)
                        .First(u => u.Id == this.Id).Phones;
        }
        #endregion

        #region AddAddressEmail
        /// <summary>
        /// Add new email to database.
        /// </summary>
        /// <param name="email">New addres email.</param>
        public void AddAddressEmail(UserEmailAddress email)
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
        #region GetEmailsAddresses
        /// <summary>
        /// Make list with email addresses of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with email addresses.</returns>
        public IList<UserEmailAddress> GetEmailsAddresses()
        {
            _context = new ApplicationDbContext();
            return  _context.People
                        .OfType<User>()
                        .Include(u => u.EmailAddresses)
                        .First(u => u.Id == this.Id).EmailAddresses;
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
