using System.Collections.Generic;
using System;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the email address of all people (<see cref="Person"/>) in the application.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Email address number Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Person's email address.
        /// </summary>
        public string AddressEmail { get; set; }
        /// <summary>
        /// Note to email address.
        /// </summary>
        public string Note { get; set; }

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
        public static IEnumerable<EmailAddress> GetUserEmails(int id)
        {
            throw new NotImplementedException();
            //return _context.Emails.ToList().Where(u => u.Person.PersonId == id);
        }
        #endregion
        #region GetEmailById
        /// <summary>
        /// Getting from database email with specific number id from database.
        /// </summary>
        /// <param name="emailId">Email id number in database</param>
        /// <returns>Email with given id number</returns>
        public static EmailAddress GetEmailById(int emailId)
        {
            throw new NotImplementedException();
            //return _context.Emails.FirstOrDefault(e => e.Id == emailId);
        }
        #endregion

        #region AddEmail
        /// <summary>
        /// Add new email to database.
        /// </summary>
        /// <param name="email">New addres email.</param>
        public static void AddEmail(EmailAddress email)
        {
            throw new NotImplementedException();
            //if (email != null)
            //{
            //    //TODO: try - catch block is needed ... when excetion will be catch than send false.
            //    _context.Emails.Add(email);
            //    _context.SaveChanges();
            //}
        }
        #endregion
        #region UpdateEmail
        /// <summary>
        /// Update in databse current address email.
        /// </summary>
        /// <param name="newEmail">New email address.</param>
        public bool UpdateEmail(string newEmail)
        {
            throw new NotImplementedException();
            //var currentEmail = _context.Emails.SingleOrDefault(e => e.Id == this.Id);
            //if (currentEmail != null)
            //{
            //    //TODO: try - catch block is needed ... when excetion will be catch than send false.
            //    currentEmail.AddressEmail = newEmail;
            //    _context.SaveChanges();
            //    return true;
            //}
            //else return false;
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete current email from database.
        /// </summary>
        public bool Delete()
        {
            throw new NotImplementedException();
            //var currentEmail = _context.Emails.Where(p => p.Id == this.Id).First();
            //if (currentEmail != null)
            //{
            //    //TODO: try - catch block is needed ... when excetion will be catch than send false.
            //    _context.Emails.Remove(currentEmail);
            //    _context.SaveChanges();
            //    return true;
            //}
            //else return false;
        }
        #endregion
    }
}
