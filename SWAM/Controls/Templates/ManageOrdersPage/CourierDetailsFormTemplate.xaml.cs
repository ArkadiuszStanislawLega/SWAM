using System.Windows.Controls;
using System.Windows.Media;

namespace SWAM.Controls.Templates.ManageOrdersPage
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

        public void SetGrayFields()
        {
            CourierName.Background = Brushes.Gray;
            CourierEmailAddress.Background = Brushes.Gray;
            CourierPhoneNumber.Background = Brushes.Gray;
        }
    }
}
