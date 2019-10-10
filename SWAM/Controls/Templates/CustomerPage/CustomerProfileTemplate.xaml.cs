using System.Windows.Controls;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfileTemplate : UserControl
    {
        public CustomerProfileTemplate()
        {
            InitializeComponent();
        }

        private void EditCustomerNameButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CancelChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void EditCustomerSurnameButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeSurname_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CancelChangeSurname_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void EditCustomerPhoneButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangePhone_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CancelChangePhone_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void EditCustomerEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CancelChangeEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ChangeDeliveryAddress_Click(object sender, System.Windows.RoutedEventArgs e) => DeliveryAddress.ShowEditControls();

        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) => ResidentalAddress.ShowEditControls();
    }
}
