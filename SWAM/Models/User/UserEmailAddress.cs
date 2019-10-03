using System.ComponentModel.DataAnnotations.Schema;
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

            _context.EmailAddresses.Remove(emailAddresses);

            if (_context.SaveChanges() == 2)
                return true;

            return false;
        }
        #endregion
    }
}
