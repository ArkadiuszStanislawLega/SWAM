using SWAM.Strings;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace SWAM.Models.ExternalSupplier
{
    public class ExternalSupplierPhone
    {
        /// <summary>
        /// Identification number from the phone number database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Note regarding the telephone number.
        /// </summary>
        public string Note { get; set; }

        public ExternalSupplier ExternalSupplier { get; set; }
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
        public bool UpdateNumber(ExternalSupplierPhone externalSupplierPhone, string newPhoneNumber)
        {
            if (externalSupplierPhone != null)
            {
                if (externalSupplierPhone.PhoneNumber == newPhoneNumber)
                    return true;

                Context.ExternalSupplierPhones.FirstOrDefault(e => e.Id == externalSupplierPhone.Id).PhoneNumber = newPhoneNumber;

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
        public bool UpdateNote(ExternalSupplierPhone externalSupplierPhone, string newNote)
        {
            if (externalSupplierPhone != null)
            {
                if (externalSupplierPhone.Note == newNote)
                    return true;

                Context.ExternalSupplierPhones.FirstOrDefault(e => e.Id == externalSupplierPhone.Id).Note = newNote;

                if (Context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region RemoveFromDb
        /// <summary>
        /// Remove current number from database.
        /// </summary>
        /// <returns>True if phone is correctly removed from database.</returns>
        public bool RemoveFromDb()
        {
            Context = new ApplicationDbContext();

            if(Context.ExternalSupplierPhones.FirstOrDefault(u => u.Id == this.Id) is ExternalSupplierPhone phone)
            {
                Context.ExternalSupplierPhones.Remove(phone);
                //Two rows are affected in database.
                //One in Phone table and one in UserPhone table.
                if (Context.SaveChanges() == 2)
                    return true;
            }

            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the phone number from the database.
        /// </summary>
        /// <param name="id">External supplier phone id in database.</param>
        /// <returns>Specific full external supplier phone from database.</returns>
        public ExternalSupplierPhone Get(int id)
        {
            Context = new ApplicationDbContext();
            return Context.ExternalSupplierPhones
                .Include(e => e.ExternalSupplier)
                .FirstOrDefault(e => e.Id == id);
        }
        #endregion
    }
}
