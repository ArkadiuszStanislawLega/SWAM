using SWAM.Strings;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace SWAM.Models.User
{
    [Table("UsersPhones")]
    public class UserPhone : Phone
    {
        public User User { get; set; }

        #region Database connection
        private static ApplicationDbContext dbContext = new ApplicationDbContext();
        private static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return dbContext;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => dbContext = value;
        }
        #endregion
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

                Context.People
                            .OfType<User>()
                            .Include(u => u.Phones)
                            .First(u => u.Id == userPhone.User.Id)
                            .Phones.First(p => p.Id == userPhone.Id)
                            .PhoneNumber = newPhoneNumber;
                if (Context.SaveChanges() == 1)
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

                Context.People
                         .OfType<User>()
                         .Include(u => u.Phones)
                         .First(u => u.Id == userPhone.User.Id)
                         .Phones.First(p => p.Id == userPhone.Id)
                         .Note = newNote;
                if (Context.SaveChanges() == 1)
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
                Context = new ApplicationDbContext();

                var phones = Context.Phones.FirstOrDefault(u => u.Id == userPhone.Id);

                if (phones != null)
                {
                    Context.Phones.Remove(phones);
                    //Two rows are affected in database.
                    //One in Phone table and one in UserPhone table.
                    if (Context.SaveChanges() == 2)
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
            Context = new ApplicationDbContext();
            return Context.People
                .OfType<User>()
                .Include(u => u.Phones)
                .FirstOrDefault(u => u.Id == userPhone.User.Id)
                .Phones
                .FirstOrDefault(u => u.Id == userPhone.Id);
        }
        #endregion
    }
}
