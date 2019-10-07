using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.User
{
    [Table("UsersPhones")]
    public class UserPhone : Phone
    {
        public User User { get; set; }

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

        #region UpdateNumber
        /// <summary>
        /// Update current phone number.
        /// </summary>
        /// <param name="newPhoneNumber">New phone/edited number.</param>
        /// <returns>True if update of phone number is corectly edited.</returns>
        public bool UpdateNumber(UserPhone userPhone, string newPhoneNumber)
        {
            if (userPhone != null)
            {
                if (userPhone.PhoneNumber == newPhoneNumber)
                    return true;

                _context.People
                            .OfType<User>()
                            .Include(u => u.Phones)
                            .First(u => u.Id == userPhone.User.Id)
                            .Phones.First(p => p.Id == userPhone.Id)
                            .PhoneNumber = newPhoneNumber;
                if (_context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region UpdateNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        /// <returns>True if update of phone number is corectly edited.</returns>
        public bool UpdateNote(UserPhone userPhone, string newNote)
        {
            if (userPhone != null)
            {
                if (userPhone.Note == newNote)
                    return true;

                _context.People
                         .OfType<User>()
                         .Include(u => u.Phones)
                         .First(u => u.Id == userPhone.User.Id)
                         .Phones.First(p => p.Id == userPhone.Id)
                         .Note = newNote;
                if (_context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete from database current number.
        /// </summary>
        /// <param name="userPhone">Phone to remove.</param>
        /// <returns>True if phone is correctly removed from database.</returns>
        public bool Delete(UserPhone userPhone)
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
        #region Get
        /// <summary>
        /// Retrieves the phone number from the database.
        /// </summary>
        /// <param name="userPhone">User phone with user Id and user phone Id from database.</param>
        /// <returns>Specific full user phone from database.</returns>
        public UserPhone Get(UserPhone userPhone)
        {
            _context = new ApplicationDbContext();
            return _context.People
                .OfType<User>()
                .Include(u => u.Phones)
                .FirstOrDefault(u => u.Id == userPhone.User.Id)
                .Phones
                .FirstOrDefault(u => u.Id == userPhone.Id);
        }
        #endregion
    }
}
