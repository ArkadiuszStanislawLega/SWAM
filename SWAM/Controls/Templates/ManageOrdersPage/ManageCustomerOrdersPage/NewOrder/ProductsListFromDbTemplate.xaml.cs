using SWAM.Models;
using SWAM.Models.Customer;
using SWAM.Models.ManageOrdersPage;
using SWAM.Models.ProductOrderList;
using SWAM.Models.ViewModels.CreateNewCustomerOrder;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.NewOrder
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

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void AddToShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            if (!(((FrameworkElement)sender).DataContext is State state))
                return;

            if (state.Available <= 0 || state.Quantity <= 0)
                return;

            var customerOrderPosition = new CustomerOrderPosition
            {
                Product = state.Product,
                ProductId = state.Id,
                Price = state.Product.Price,
                Quantity = 1,
                State = state
            };

            ProductOrderListViewModel.Instance.Add(customerOrderPosition);
            PaymentOrderViewModel.Instance.Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UserDependsAccessProductListViewModel.Instance.Refresh();
        }

        private void WarehouseDatagridFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox filterTextBox = sender as TextBox;
            string searchingText = filterTextBox.Text;
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(States.ItemsSource);

            if (searchingText == string.Empty)
                collectionView.Filter = null;
            else
            {
                collectionView.Filter = @object =>
                {
                    State state = @object as State;
                    return (state.Product.Name.ToUpper().StartsWith(searchingText.ToUpper()));
                };
            }
        }
    }
}
