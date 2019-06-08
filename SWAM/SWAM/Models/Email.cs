using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Email
    {
        int _id;
        string _addressEmail;
        User _user;

        public int Id { get => _id; set => _id = value; }
        public string AddressEmail { get => _addressEmail; set => _addressEmail = value; }
        public User User { get => _user; set => _user = value; }

        #region UpdateEmail
        /// <summary>
        /// Update in databse current address email.
        /// </summary>
        /// <param name="newEmail">New email address.</param>
        public void UpdateEmail(string newEmail)
        {
            //TODO: Catch exception - Phone - UpdateNumber
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentEmail = context.Emails.SingleOrDefault(e => e.Id == this._id);
                if (currentEmail != null)
                {
                    currentEmail.AddressEmail = newEmail;
                    context.SaveChanges();
                }
            };
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete current email from database.
        /// </summary>
        public void Delete()
        {
            //TODO: Catch exception - Phone - Delete
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentEmail = context.Emails.Where(p => p.Id == this._id).First();
                if (currentEmail != null)
                {
                    context.Emails.Remove(currentEmail);
                    context.SaveChanges();
                }
            };
        }
        #endregion
    }
}
