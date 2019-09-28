using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWAM.Controls.Windows;
using SWAM.Enumerators;
using SWAM.Models;
using SWAM.Models.ProductPage;
using SWAM.Windows;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : UserControl
    {
        enum Operation { edit, add };

        Operation _currentOperation;
        private long _weight;
        private long _lenght;
        private long _width;
        private long _height;
        private decimal _price;
      
        private ProductListViewModel _productList = new ProductListViewModel();
        private ApplicationDbContext _context = new ApplicationDbContext();
        public ApplicationDbContext context
        {
            get
            {
                //TODO: Try catch block
                return this._context;
            }
        }

        #region BasicConstructor
        public ManageItemPage()
        {
            InitializeComponent();
            DataContext = this._productList;
            this.ProductsList.MouseDown += ProductsList_MouseDown;

            if(this._productList.Products.Count > 0)
                this.ProductProfile.DataContext = this._productList.Products[0];
        }

        private void ProductsList_MouseDown(object sender, MouseButtonEventArgs e)
            => this.ProductProfile.DataContext = this.ProductsList.SelectedItem;

        #endregion

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this._currentOperation = Operation.edit;
            this.tboxName.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.tboxName.Visibility = Visibility.Visible;

            if (ValidationTextBoxes())
            {
                if (this._currentOperation == Operation.add)
                {
                    context.Products.Add(new Models.Product()
                    {
                        Name = this.tbName.Text,
                        Weigth = this._weight,
                        Length = this._lenght,
                        Width = this._width,
                        Height = this._height,
                        Price = this._price
                    });
                    context.SaveChanges();
                }
                else
                {
                    if (ProductProfile.DataContext is Product product)
                    {
                        var dbproduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                        dbproduct.Name = this.tbName.Text;
                        dbproduct.Weigth = this._weight;
                        dbproduct.Length = this._lenght;
                        dbproduct.Width = this._width;
                        dbproduct.Height = this._height;
                        dbproduct.Price = this._price;
                        context.SaveChanges();
                    }
                }
            }

            this._productList.Refresh();
        }

        private bool ValidationTextBoxes()
        {
            if (long.TryParse(this.tbWeight.Text, out this._weight) && this._weight > 0)
            {
                if (long.TryParse(this.tbLenght.Text, out this._lenght) && this._lenght > 0)
                {
                    if (long.TryParse(this.tbWidth.Text, out this._width) && this._width > 0)
                    {
                        if (long.TryParse(this.tbHeight.Text, out this._height) && this._height > 0)
                        {
                            if (decimal.TryParse(this.tbPrice.Text, out this._price) && this._price > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this._currentOperation = Operation.add;
            this.tboxName.Visibility = Visibility.Collapsed;

            this.tbName.Text = string.Empty;
            this.tbLenght.Text = string.Empty;
            this.tbHeight.Text = string.Empty;
            this.tbWidth.Text = string.Empty;
            this.tbWeight.Text = string.Empty;
            this.tbPrice.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductProfile.DataContext is Product product)
            {
                var dbproduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                context.Products.Remove(dbproduct);
                context.SaveChanges();

                this._productList.Refresh();
            }
        }

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
