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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var phone = DataContext as Phone;

            phone.UpdateNumber(EditPhoneNumber.Text);
            phone.UpdateNote(EditNote.Text);

            TurnOn(this.PhoneNumber);
            TurnOn(this.Note);

            TurnOff(this.EditPhoneNumber);
            TurnOff(this.EditNote);

            this.Confirm.IsEnabled = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Refresh list after delete.
            var phone = DataContext as Phone;
            phone.Delete();
        }
    }
}
