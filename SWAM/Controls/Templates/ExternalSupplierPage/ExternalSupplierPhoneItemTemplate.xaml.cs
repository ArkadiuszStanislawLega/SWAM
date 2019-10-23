using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Exceptions;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;
using System.Windows;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSupplierPhoneItemTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierPhoneItemTemplate : BasicUserControl
    {
        #region Properties
        /// <summary>
        /// Phone numbe before edit.
        /// </summary>
        private string _phone;
        /// <summary>
        /// Phone note before edit.
        /// </summary>
        private string _note;
        #endregion

        public ExternalSupplierPhoneItemTemplate()
        {
            InitializeComponent();
        }

        #region Confirm_Click
        /// <summary>
        /// Action after click button to save changes after 
        /// edit phone number from list of phones in user profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplierPhone phone)
            {
                //Get phone number from database before edit.
                if (phone.Get(phone.Id) is ExternalSupplierPhone phoneNumberBeforeEdited)
                {
                    this._confirmWindow.Show($"Czy jesteś pewien że chcesz zmienić numer telefonu {phoneNumberBeforeEdited.Note} - {phoneNumberBeforeEdited.PhoneNumber} na {this.EditNote.Text} - {this.EditPhoneNumber.Text}?",
                        out bool isEditConfirmed, $"Potwierdź edycję numeru telefonu {phoneNumberBeforeEdited.ExternalSupplier.Name}");
                    //if the user has confirmed editing the telephone number
                    if (isEditConfirmed)
                    {
                        if (phone.UpdateNumber(phoneNumberBeforeEdited, this.EditPhoneNumber.Text))
                        {
                            if (phone.UpdateNote(phoneNumberBeforeEdited, this.EditNote.Text))
                                InformationToUser($"Numer telefonu {phoneNumberBeforeEdited.ToString()} edytowano na {this.EditNote.Text} - {this.EditPhoneNumber.Text}.");

                            else
                                InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PHONE_ERROR} W czasie zmiany notatki telefonu. {ErrorMesages.DATABASE_ERROR}", true);
                        }
                        else
                            InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PHONE_ERROR} W czasie zmiany numeru telefonu. {ErrorMesages.DATABASE_ERROR}", true);
                    }
                }
                else
                    InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PHONE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
            }
            else InformationToUser(ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PHONE_ERROR, true);
        }
        #endregion
        #region Delete_Click
        /// <summary>
        /// Action after click button delete phone number from list of phones in user profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplierPhone phone)
            {
                if (this._confirmWindow != null)
                {
                    this._confirmWindow.Show($"Czy na pewno chcesz usunąc numer telefonu {phone.ToString()}?", out bool isConfirmed, "Potwierdź usunięcie kontaktu");
                    if (isConfirmed)
                    {
                        if (phone.RemoveFromDb())
                        {
                            InformationToUser($"Usunięto numer telefonu {phone.Note} - {phone.PhoneNumber}.");

                            //Refresh phones list.
                            try
                            {
                                var phoneList = SWAM.MainWindow.FindParent<ExternalSuppliersEditablePhoneListTemplate>(this);
                                if (phoneList != null)
                                    phoneList.RefreshPhoneList();
                                else throw new RefreshExternalsupplierPhonesListException();
                            }
                            catch (RefreshExternalsupplierPhonesListException ex) { ex.ShowMessage(this); }
                        }
                        else InformationToUser($"{ErrorMesages.DELETE_EXTERNAL_SUPPLIER_PHONE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
                    }
                }
                else InformationToUser($"{ErrorMesages.DELETE_EXTERNAL_SUPPLIER_PHONE_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DELETE_EXTERNAL_SUPPLIER_PHONE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region Cancel_Click
        /// <summary>
        /// Action after click cancel button during editing phone number or note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.EditNote.Text = this._note;
            this.EditPhoneNumber.Text = this._phone;

            this.Note.Text = this._note;
            this.PhoneNumber.Text = this._phone;
        }
        #endregion
        #region Edit_Click
        /// <summary>
        /// Action after click edit contact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this._phone = this.PhoneNumber.Text;
            this._note = this.Note.Text;
        }
        #endregion
    }
}
