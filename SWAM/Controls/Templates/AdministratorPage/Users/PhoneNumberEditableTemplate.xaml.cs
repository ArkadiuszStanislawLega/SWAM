using System.Windows;
using SWAM.Strings;
using SWAM.Exceptions;
using SWAM.Models.User;


namespace SWAM.Controls.Templates.AdministratorPage
{
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumberEditableTemplate.xaml
    /// </summary>
    public partial class PhoneNumberEditableTemplate : BasicUserControl
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

        public PhoneNumberEditableTemplate()
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
            if (DataContext is UserPhone phone)
            {
                phone.User.UpdatePhoneNumber(phone, EditPhoneNumber.Text);
                phone.User.UpdatePhoneNote(phone, EditNote.Text);
                 
                InformationToUser($"Edytowano numer telefonu {EditNote.Text} - {EditPhoneNumber.Text}.");
            }
            else InformationToUser(ErrorMesages.DURING_EDIT_PHONE_ERROR, true);
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
            if (DataContext is UserPhone phone)
            {
                if (this._confirmWindow != null)
                {
                    this._confirmWindow.Show($"Czy na pewno chcesz usunąc numer telefonu {phone.ToString()}?", out bool isConfirmed, "Potwierdź usunięcie kontaktu");
                    if (isConfirmed)
                    {
                        if (phone.User.DeletePhone(phone))
                        {
                            InformationToUser($"Usunięto numer telefonu {phone.Note} - {phone.PhoneNumber}.");

                            //Refresh phones list.
                            try
                            {
                                var phoneList = FindParent<PhoneNumbersEditableListTemplate>(this);
                                if (phoneList != null)
                                    phoneList.RefreshPhoneList();
                                else throw new RefreshUserPhonesListException();
                            }
                            catch (RefreshUserPhonesListException ex) { ex.ShowMessage(this); }
                        }
                        else InformationToUser($"{ErrorMesages.DURING_DELETE_PHONE_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
                    }
                }
                else InformationToUser($"{ErrorMesages.DURING_DELETE_PHONE_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_DELETE_PHONE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
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
