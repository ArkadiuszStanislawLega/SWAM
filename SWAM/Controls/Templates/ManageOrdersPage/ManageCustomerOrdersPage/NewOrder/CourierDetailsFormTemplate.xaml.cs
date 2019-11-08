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

        public void SetFieldsGray()
        {
            CourierTIN.Background = Brushes.Gray;
            CourierName.Background = Brushes.Gray;
            CourierEmailAddress.Background = Brushes.Gray;
            CourierPhoneNumber.Background = Brushes.Gray;
        }

        public void SetFieldsWhite()
        {
            CourierTIN.Background = Brushes.White;
            CourierName.Background = Brushes.White;
            CourierEmailAddress.Background = Brushes.White;
            CourierPhoneNumber.Background = Brushes.White;
        }
    }
}
