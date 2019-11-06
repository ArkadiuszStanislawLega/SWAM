using SWAM.Models.ViewModels.CreateNewWarehouseOrder;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder
{
    /// <summary>
    /// Interaction logic for ProductsListFromDbTemplate.xaml
    /// </summary>
    public partial class ProductsListFromDbTemplate : UserControl
    {
        public ProductsListFromDbTemplate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductListViewModel.Instance.Refresh();
        }

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void AddToShoppingCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
