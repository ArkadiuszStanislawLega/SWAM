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
    public partial class PhoneNumberEditableTemplate : UserControl
    {
        /// <summary>
        /// Information about actions to user.
        /// </summary>
        private string _message;

        public PhoneNumberEditableTemplate()
        {
            InitializeComponent();
        }

        #region Edit_Click
        /// <summary>
        /// Action after edit button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            TurnOff(this.PhoneNumber);
            TurnOff(this.Note);

            TurnOn(this.EditPhoneNumber);
            TurnOn(this.EditNote);
       
            this.Confirm.IsEnabled = true;
        }
        #endregion
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

                this._message = $"Edytowano numer telefonu {EditNote.Text} - {EditPhoneNumber.Text}.";
                InformationToUser();

                //TODO: Make this in xaml.
                TurnOn(this.PhoneNumber);
                TurnOn(this.Note);

                TurnOff(this.EditPhoneNumber);
                TurnOff(this.EditNote);

                this.Confirm.IsEnabled = false;
            }
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

                this._message = $"Usunięto numer telefonu {phone.Note} - {phone.PhoneNumber}.";
                InformationToUser();

                var phoneList = FindParent<PhoneNumbersEditableListTemplate>(this);
                if (phoneList != null)
                    phoneList.RefreshPhoneList();
            }
        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// Make information in MainWindow to user about action.
        /// </summary>
        private void InformationToUser()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                    mainWindow.InformationForUser(this._message);
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
