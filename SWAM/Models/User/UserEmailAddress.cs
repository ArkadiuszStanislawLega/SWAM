using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.User
{
    [Table("UsersEmailAddresses")]
    public class UserEmailAddress : EmailAddress
    {
        public User User { get; set; }

        #region Delete
        /// <summary>
        /// Delete current email from database.
        /// </summary>
        public bool Delete()
        {
            //TODO: try - catch block is needed ... when excetion will be catch than send false.
            var emailAddresses = _context.EmailAddresses.First(e => e.Id == this.Id);
            if (emailAddresses != null)
            {
                _context.EmailAddresses.Remove(emailAddresses);

                if (_context.SaveChanges() == 2)
                    return true;
            }

            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the user's email address from the database after the address Id number.
        /// </summary>
        /// <param name="emailAddressId">Id Email Address.</param>
        /// <returns>Specific user email address from database.</returns>
        public UserEmailAddress Get(int emailAddressId)
        {
            _context = new ApplicationDbContext();
            return _context.People.OfType<User>()
               .Include(u => u.EmailAddresses)
               .First(u => u.Id == this.User.Id)
               .EmailAddresses.First(e => e.Id == emailAddressId);
        }
        #endregion
        #region Update
        /// <summary>
        /// Change specific email address of user to new one.
        /// </summary>
        /// <param name="emailAddress">New email address.</param>
        /// <returns>True if the email address could be changed.</returns>
        public bool Update(string emailAddress)
        {
            _context = new ApplicationDbContext();

            User dbEmailOwner = _context.People
                .OfType<User>()
                .Include(u => u.EmailAddresses)
                .First(u => u.Id == this.User.Id);

            if (dbEmailOwner != null)
            {
                if (dbEmailOwner.EmailAddresses != null)
                {
                    dbEmailOwner.EmailAddresses
                        .First(e => e.Id == this.Id)
                        .AddressEmail = emailAddress;

                    if (_context.SaveChanges() == 1)
                        return true;
                }
            }

            return false;
        }
        #endregion
    }
}
