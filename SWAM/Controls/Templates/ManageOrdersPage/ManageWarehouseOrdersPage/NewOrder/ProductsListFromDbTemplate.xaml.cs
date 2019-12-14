using SWAM.Models;
using SWAM.Models.ViewModels.CreateNewWarehouseOrder;
using SWAM.Models.Warehouse;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            if (!(((FrameworkElement)sender).DataContext is Product product))
                return;

            var warehouseOrderPosition = new WarehouseOrderPosition
            {
                Product = product,
                ProductId = product.Id,
                Quantity = 1
            };

            ProductOrderListViewModel.Instance.Add(warehouseOrderPosition);
        }

        private void WarehouseDatagridFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox filterTextBox = sender as TextBox;
            string searchingText = filterTextBox.Text;
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Products.ItemsSource);

            if (searchingText == string.Empty)
                collectionView.Filter = null;
            else
            {
                collectionView.Filter = @object =>
                {
                    Product product = @object as Product;
                    return (product.Name.ToUpper().StartsWith(searchingText.ToUpper()));
                };
            }
        }
    }
}
