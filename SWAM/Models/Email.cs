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

        private static ApplicationDbContext _context 
        {
            get
            {
                //TODO: Make all exceptions
                return DB_CONTEXT;
            }
        }

        #region GetUserEmails
        /// <summary>
        /// Make list with email addresses of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with email addresses.</returns>
        public static IEnumerable<Email> GetUserEmails(int id) => _context.Emails.ToList().Where(u => u.UserId == id);
        #endregion
        #region GetEmailById
        /// <summary>
        /// Getting from database email with specific number id from database.
        /// </summary>
        /// <param name="emailId">Email id number in database</param>
        /// <returns>Email with given id number</returns>
        public static Email GetEmailById(int emailId) => _context.Emails.FirstOrDefault(e => e.Id == emailId);
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
                //TODO: try - catch block is needed ... when excetion will be catch than send false.
                _context.Emails.Add(email);
                _context.SaveChanges();
            }
        }
        #endregion
        #region UpdateEmail
        /// <summary>
        /// Update in databse current address email.
        /// </summary>
        /// <param name="newEmail">New email address.</param>
        public bool UpdateEmail(string newEmail)
        {
            var currentEmail = _context.Emails.SingleOrDefault(e => e.Id == this._id);
            if (currentEmail != null)
            {
                //TODO: try - catch block is needed ... when excetion will be catch than send false.
                currentEmail.AddressEmail = newEmail;
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete current email from database.
        /// </summary>
        public bool Delete()
        {
            var currentEmail = _context.Emails.Where(p => p.Id == this._id).First();
            if (currentEmail != null)
            {
                //TODO: try - catch block is needed ... when excetion will be catch than send false.
                _context.Emails.Remove(currentEmail);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        #endregion
    }
}
