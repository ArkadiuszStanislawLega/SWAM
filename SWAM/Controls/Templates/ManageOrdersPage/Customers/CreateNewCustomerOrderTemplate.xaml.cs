using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Interaction logic for CreateNewCustomerOrderTemplate.xaml
    /// </summary>
    public partial class CreateNewCustomerOrderTemplate : UserControl
    {
        public CreateNewCustomerOrderTemplate()
        {
            InitializeComponent();
        }

        private void SwitchContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (customerElementsContainer.Visibility == Visibility.Visible)
            {
                customerElementsContainer.Visibility = Visibility.Hidden;
                productElementsContainer.Visibility = Visibility.Visible;
            }
            else
            {
                customerElementsContainer.Visibility = Visibility.Visible;
                productElementsContainer.Visibility = Visibility.Hidden;
            }
        }
    }
}
