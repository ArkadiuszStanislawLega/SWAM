using System.Windows;
using System.Windows.Controls;


namespace SWAM.Controls.Templates.AdministratorPage
{
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

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
