using System.Windows;
using System.Windows.Controls;


namespace SWAM.Controls.Templates.AdministratorPage
{
    using SWAM.Models;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy PhoneNumberEditableTemplate.xaml
    /// </summary>
    public partial class PhoneNumberEditableTemplate : UserControl
    {
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
            var phone = DataContext as Phone;

            phone.UpdateNumber(EditPhoneNumber.Text);
            phone.UpdateNote(EditNote.Text);
            SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).InformationForUser($"Edytowano numer telefonu {EditNote.Text} - {EditPhoneNumber.Text}.");

            //TODO: Make this functions in xaml.
            TurnOn(this.PhoneNumber);
            TurnOn(this.Note);

            TurnOff(this.EditPhoneNumber);
            TurnOff(this.EditNote);

            this.Confirm.IsEnabled = false;
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
            var phone = DataContext as Phone;
            phone.Delete();

            SWAM.MainWindow.FindParent<SWAM.MainWindow>(this).InformationForUser($"Usunięto numer telefonu {phone.Note} - {phone.PhoneNumber}.");

            FindParent<PhoneNumbersEditableListTemplate>(this).RefreshPhoneList();
        }
        #endregion
    }
}
