using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models
{
    public class Email
    {
        int _id;
        string _addressEmail;

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string AddressEmail { get => _addressEmail; set => _addressEmail = value; }


        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext context()
        {
            //TODO: Make all exceptions
                return DB_CONTEXT;
        }

        #region GetUserEmails
        /// <summary>
        /// Make list with email addresses of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with email addresses.</returns>
        public static IEnumerable<Email> GetUserEmails(int id) => context().Emails.ToList().Where(u => u.UserId == id);
        #endregion

        #region AddEmail
        /// <summary>
        /// Add new email to database.
        /// </summary>
        /// <param name="email">New addres email.</param>
        public static void AddEmail(Email email)
        {
            if (email != null)
            {
                context().Emails.Add(email);
                context().SaveChanges();
            }
        }
        #endregion
        #region UpdateEmail
        /// <summary>
        /// Update in databse current address email.
        /// </summary>
        /// <param name="newEmail">New email address.</param>
        public void UpdateEmail(string newEmail)
        {
            var currentEmail = context().Emails.SingleOrDefault(e => e.Id == this._id);
            if (currentEmail != null)
            {
                currentEmail.AddressEmail = newEmail;
                context().SaveChanges();
            }
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete current email from database.
        /// </summary>
        public void Delete()
        {
            var currentEmail = context().Emails.Where(p => p.Id == this._id).First();
            if (currentEmail != null)
            {
                context().Emails.Remove(currentEmail);
                context().SaveChanges();
            }
        }
        #endregion
    }
}
