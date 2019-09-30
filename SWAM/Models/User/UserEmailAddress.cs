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
            _context.People.OfType<User>().FirstOrDefault(u => u.Id == User.Id).EmailAddresses.Remove(this);

            if (_context.SaveChanges() == 1)
                return true;
            else
                return false;
        }
        #endregion
    }
}
