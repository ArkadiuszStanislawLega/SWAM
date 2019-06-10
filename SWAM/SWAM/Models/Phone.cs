using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Phone
    {
        int _id;
        string _note;
        string _phoneNumber;

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Note { get => _note; set => _note = value; }

        #region UpdateNumber
        /// <summary>
        /// Update current phone number.
        /// </summary>
        /// <param name="newPhoneNumber">New phone/edited number.</param>
        public void UpdateNumber(string newPhoneNumber)
        {
            //TODO: Catch exception - Phone - UpdateNumber
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentPhone = context.Phones.SingleOrDefault(p => p.Id == this._id);
                if (currentPhone != null)
                {
                    currentPhone._phoneNumber = newPhoneNumber;
                    context.SaveChanges();
                }
            };
        }
        #endregion
        #region UpdateNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        public void UpdateNote(string newNote)
        {
            //TODO: Catch exception - Phone - UpdateNote
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentPhone = context.Phones.SingleOrDefault(p => p.Id == this._id);
                if (currentPhone != null)
                {
                    currentPhone._note = newNote;
                    context.SaveChanges();
                }
            };
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete from database current number.
        /// </summary>
        public void Delete()
        {
            //TODO: Catch exception - Phone - Delete
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var currentPhone = context.Phones.Where(p => p.Id == this._id).First();
                if (currentPhone != null)
                {
                    context.Phones.Remove(currentPhone);
                    context.SaveChanges();
                }
            };
        }
        #endregion
    }
}
