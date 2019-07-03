using System.Windows;
using System.Windows.Controls;


namespace SWAM.Controls.Templates.AdministratorPage
{
    using SWAM.Exceptions;
    using SWAM.Models;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumberEditableTemplate.xaml
    /// </summary>
    public partial class PhoneNumberEditableTemplate : BasicUserControl
    {
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
            if (DataContext is Phone phone)
            {
                phone.UpdateNumber(EditPhoneNumber.Text);
                phone.UpdateNote(EditNote.Text);

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
            if (DataContext is Phone phone)
            {
                if (this._confirmWindow != null)
                {
                    this._confirmWindow.Show($"Czy na pewno chcesz usunąc numer telefonu {phone.ToString()}?", out bool isConfirmed, "Potwierdź usunięcie kontaktu");
                    if (isConfirmed)
                    {
                        phone.Delete();
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
            if (DataContext is Phone phone)
            {
                Phone dbPhone = Phone.GetPhoneById(phone.Id);
                if (dbPhone != null)
                {
                    this.EditPhoneNumber.Text = dbPhone.PhoneNumber;
                    this.EditNote.Text = dbPhone.Note;

                    this.PhoneNumber.Text = dbPhone.PhoneNumber;
                    this.Note.Text = dbPhone.Note;
                }
                 else InformationToUser($"{ErrorMesages.DURING_EDIT_PHONE_ERROR} {ErrorMesages.CANCEL_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_EDIT_PHONE_ERROR} {ErrorMesages.CANCEL_ERROR}", true);
        }
        #endregion
    }
}
