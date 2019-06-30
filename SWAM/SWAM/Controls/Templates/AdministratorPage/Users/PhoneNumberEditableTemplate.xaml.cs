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
            else InformationToUser(ErrorMesages.DURING_DELETE_PHONE_ERROR, true);
        }
        #endregion
    }
}
