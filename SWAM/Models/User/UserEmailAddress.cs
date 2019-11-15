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
            var emailAddresses = context.EmailAddresses.First(e => e.Id == this.Id);
            if (emailAddresses != null)
            {
                context.EmailAddresses.Remove(emailAddresses);

                if (context.SaveChanges() == 2)
                    return true;
            }

            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the user's email address from the database after the address Id number.
        /// </summary>
        /// <returns>Specific user email address from database.</returns>
        public UserEmailAddress Get()
        {
            context = new ApplicationDbContext();
            return context.People.OfType<User>()
               .Include(u => u.EmailAddresses)
               .First(u => u.Id == this.User.Id)
               .EmailAddresses.First(e => e.Id == this.Id);
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
            context = new ApplicationDbContext();

            User dbEmailOwner = context.People
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

                    if (context.SaveChanges() == 1)
                        return true;
                }
            }

            return false;
        }
        #endregion
    }
}
