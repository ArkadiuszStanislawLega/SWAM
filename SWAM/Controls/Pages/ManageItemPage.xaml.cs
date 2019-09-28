using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SWAM.Models;
using SWAM.Models.ProductPage;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : UserControl
    {
        enum Operation { none, edit, add };

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
        #endregion
        private void ProductsList_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            this.ProductProfile.DataContext = this.ProductsList.SelectedItem;
            this.EditedName.Visibility = Visibility.Visible;
            this._currentOperation = Operation.none;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this._currentOperation = Operation.edit;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationTextBoxes())
            {
                if (this._currentOperation == Operation.add)
                {
                    context.Products.Add(new Models.Product()
                    {
                        Name = this.EditedName.Text,
                        Weigth = this._weight,
                        Length = this._lenght,
                        Width = this._width,
                        Height = this._height,
                        Price = this._price
                    });
                    context.SaveChanges();
                }
                else if(this._currentOperation == Operation.edit)
                {
                    if (ProductProfile.DataContext is Product product)
                    {
                        var dbproduct = context.Products.FirstOrDefault(p => p.Id == product.Id);
                        dbproduct.Name = this.EditedName.Text;
                        dbproduct.Weigth = this._weight;
                        dbproduct.Length = this._lenght;
                        dbproduct.Width = this._width;
                        dbproduct.Height = this._height;
                        dbproduct.Price = this._price;
                        context.SaveChanges();
                    }
                }
                this._productList.Refresh();
            }
        }

        private bool ValidationTextBoxes()
        {
            if (long.TryParse(this.EditedWeight.Text, out this._weight) && this._weight > 0)
            {
                if (long.TryParse(this.EditedLenght.Text, out this._lenght) && this._lenght > 0)
                {
                    if (long.TryParse(this.EditedWidth.Text, out this._width) && this._width > 0)
                    {
                        if (long.TryParse(this.EditedHeight.Text, out this._height) && this._height > 0)
                        {
                            if (decimal.TryParse(this.EditedPrice.Text, out this._price) && this._price > 0)
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

            this.EditedName.Text = string.Empty;
            this.EditedLenght.Text = string.Empty;
            this.EditedHeight.Text = string.Empty;
            this.EditedWidth.Text = string.Empty;
            this.EditedWeight.Text = string.Empty;
            this.EditedPrice.Text = string.Empty;
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

        private void NumberRowIteration(object sender, DataGridRowEventArgs e) => e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    
    }
}
