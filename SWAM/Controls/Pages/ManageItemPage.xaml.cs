using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.ProductListViewModel;
using SWAM.Strings;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : BasicUserControl
    {
        /// <summary>
        /// Enumerator to check what operation will be performed.
        /// </summary>
        enum Operation { none, edit, add };

        #region Properties
        /// <summary>
        /// The operation that will be performed.
        /// </summary>
        Operation _currentOperation;
        /// <summary>
        /// Property needed to check if the correct weight value has been entered.
        /// </summary>
        private long _weight;
        /// <summary>
        /// Property needed to check if the correct lenght value has been entered.
        /// </summary>
        private long _lenght;
        /// <summary>
        /// Property needed to check if the correct width value has been entered.
        /// </summary>
        private long _width;
        /// <summary>
        /// Property needed to check if the correct height value has been entered.
        /// </summary>
        private long _height;
        /// <summary>
        /// Property needed to check if the correct price value has been entered.
        /// </summary>
        private decimal _price;
        /// <summary>
        /// List view model of all products in database.
        /// </summary>
        private ProductListViewModel _productList = new ProductListViewModel();
        /// <summary>
        /// Connection to database.
        /// </summary>
        private ApplicationDbContext _context = new ApplicationDbContext();
        /// <summary>
        /// Connection to database.
        /// </summary>
        public ApplicationDbContext context
        {
            get
            {
                //TODO: Try catch block
                return this._context;
            }
        }
        #endregion
        #region BasicConstructor
        public ManageItemPage()
        {
            InitializeComponent();
            DataContext = this._productList;

            if(this._productList.Products.Count > 0)
                this.ProductProfile.DataContext = this._productList.Products[0];
        }
        #endregion
        #region ProductsList_LeftMouseButtonDown
        /// <summary>
        /// Action after click item in product list.
        /// </summary>
        /// <param name="sender">Item from ProductListViewModel</param>
        /// <param name="e">Mouse button click</param>
        private void ProductsList_LeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ProductProfile.DataContext = this.ProductsList.SelectedItem;
            if (this.EditedName.Visibility == Visibility.Visible)
            {
                Storyboard hideStory = (Storyboard)this.FindResource("HideEditStory");
                hideStory.Begin();
            }
            this._currentOperation = Operation.none;
        }
        #endregion
        #region EditButton_Click
        /// <summary>
        /// Action after click Edit button.
        /// </summary>
        /// <param name="sender">Edit button.</param>
        /// <param name="e">Click action.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e) => this._currentOperation = Operation.edit;
        #endregion
        #region SaveButton_Click
        /// <summary>
        /// Action after click save button.
        /// Depending on which button was previously pressed, the action is performed, either adding a new product or editing.
        /// </summary>
        /// <param name="sender">Save button.</param>
        /// <param name="e">Clicked.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationTextBoxes())
            {
                if (this._currentOperation == Operation.add)
                {
                    Product.AddNewProduct(new Models.Product()
                    {
                        Name = this.EditedName.Text,
                        Weigth = this._weight,
                        Length = this._lenght,
                        Width = this._width,
                        Height = this._height,
                        Price = this._price
                    });                    
                }
                else if(this._currentOperation == Operation.edit)
                {
                    if (ProductProfile.DataContext is Product product)
                    {
                        this._confirmWindow.Show($"Potwierdź edycję produktu {this.EditedName.Text}?", out bool isConfirmed, $"Edytuj {this.EditedName.Text}");
                        if (isConfirmed)
                        {
							Product.EditProduct(product);                            
                        }
                    }
                    else InformationToUser($"{ErrorMesages.DURING_EDIT_PRODUCT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
                }
                this._productList.Refresh();
            }
        }
        #endregion
        #region AddButton_Click
        /// <summary>
        /// Action after click add button. 
        /// Clears all EditBoxes.
        /// </summary>
        /// <param name="sender">Button add new item</param>
        /// <param name="e">Event click add new item button.</param>
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
        #endregion
        #region DeleteButton_Click
        /// <summary>
        /// Action after click delete product.
        /// Removes the specified product from the database.
        /// </summary>
        /// <param name="sender">Button delete product.</param>
        /// <param name="e">Event click delete button.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductProfile.DataContext is Product product)
            {
                this._confirmWindow.Show($"Czy jesteś pewien że chcesz usunąć {product.Name}?", out bool isConfirmed, $"Usuń {product.Name}");
                if (isConfirmed)
                {
					Product.DeleteProduct(product);	
					this._productList.Refresh();
                }
            }
            else InformationToUser($"{ErrorMesages.DURING_DELETE_PRODUCT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion

        #region ValidationTextBoxes
        /// <summary>
        /// Validate fields that should be filled. They must be long digits, only at the price it must be decimal.
        /// </summary>
        /// <returns>True if all numbers are correct.</returns>
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
        #endregion

        private void NumberRowIteration(object sender, DataGridRowEventArgs e) => e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    
    }
}
