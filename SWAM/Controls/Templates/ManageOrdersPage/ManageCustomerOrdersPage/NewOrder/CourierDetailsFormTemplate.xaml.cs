using System.Windows.Controls;
using System.Windows.Media;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder
{
    /// <summary>
    /// Interaction logic for CourierTemplate.xaml
    /// </summary>
    public partial class CourierDetailsFormTemplate : UserControl
    {
        public CourierDetailsFormTemplate()
        {
            InitializeComponent();
        }

        public void HideFields()
        {
            CourierTIN.Visibility = System.Windows.Visibility.Hidden;
            CourierName.Visibility = System.Windows.Visibility.Hidden;
            CourierEmailAddress.Visibility = System.Windows.Visibility.Hidden;
            CourierPhoneNumber.Visibility = System.Windows.Visibility.Hidden;
        }

        public void ShowFields()
        {
            CourierTIN.Visibility = System.Windows.Visibility.Visible;
            CourierName.Visibility = System.Windows.Visibility.Visible;
            CourierEmailAddress.Visibility = System.Windows.Visibility.Visible;
            CourierPhoneNumber.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
