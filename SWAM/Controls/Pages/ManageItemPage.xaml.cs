﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SWAM.Controls.Pages;
using SWAM.Models;
using SWAM.Models.ProductListViewModel;
using SWAM.Strings;

namespace SWAM
{
	/// <summary>
	/// Logika interakcji dla klasy ManageItemPage.xaml
	/// </summary>
	public partial class ManageItemPage : BasicPage
	{
		#region Properties
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
		#endregion
		#region BasicConstructor
		public ManageItemPage()
		{
			InitializeComponent();

			if (ProductListViewModel.Instance.Products.Count > 0)
				this.ProductProfile.DataContext = ProductListViewModel.Instance.Products[0];
			
			if (MainWindow.LoggedInUser != null && (MainWindow.LoggedInUser.Permissions == Enumerators.UserType.Warehouseman || MainWindow.LoggedInUser.Permissions == Enumerators.UserType.Seller))
			{
				AddButton.IsEnabled = false;
				EditButton.IsEnabled = false;
				DeleteButton.IsEnabled = false;
			}

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

			if (MainWindow.LoggedInUser.Permissions == Enumerators.UserType.Warehouseman || MainWindow.LoggedInUser.Permissions == Enumerators.UserType.Seller)
			{
				AddButton.IsEnabled = false;
				EditButton.IsEnabled = false;
				DeleteButton.IsEnabled = false;
			}
		}
		#endregion
		#region EditButton_Click
		/// <summary>
		/// Action after click Edit button.
		/// </summary>
		/// <param name="sender">Edit button.</param>
		/// <param name="e">Click action.</param>
		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (ProductProfile.DataContext is Product product)
			{
				if (product.Get() != null)
				{
					this.AddProductToDbButton.IsEnabled = true;
				}
			}
		}
		#endregion
		#region SaveButton_Click
		/// <summary>
		/// Action after click save button.
		/// Depending on which button was previously pressed, the action is performed, either adding a new product or editing.
		/// </summary>
		/// <param name="sender">Save button.</param>
		/// <param name="e">Clicked.</param>
		private void AddProductToDbButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidationTextBoxes())
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
				this.ProductProfile.DataContext = ProductListViewModel.Instance.LastProduct();

				ProductListViewModel.Instance.Refresh();
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
		private void AddButton_Click(object sender, RoutedEventArgs e) => ClearData();
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
				if (product.Get() != null)
				{
					this._confirmWindow.Show($"Czy jesteś pewien że chcesz usunąć {product.Name}?", out bool isConfirmed, $"Usuń {product.Name}");
					if (isConfirmed)
					{
						Product.DeleteProduct(product);
						ProductListViewModel.Instance.Refresh();
						ClearData();
						EditButton.IsEnabled = false;
					}
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

		#region ClearData
		/// <summary>
		/// Clear all edited fields.
		/// </summary>
		private void ClearData()
		{
			this.ProductProfile.DataContext = null;
			this.EditedName.Text = string.Empty;
			this.EditedLenght.Text = string.Empty;
			this.EditedHeight.Text = string.Empty;
			this.EditedWidth.Text = string.Empty;
			this.EditedWeight.Text = string.Empty;
			this.EditedPrice.Text = string.Empty;
		}
		#endregion

		/// <summary>
		/// Refreshing data.
		/// </summary>
		public override void RefreshData() => ProductListViewModel.Instance.Refresh();
		private void NumberRowIteration(object sender, DataGridRowEventArgs e) => e.Row.Header = (e.Row.GetIndex() + 1).ToString();

		private void RefreshProductListButton_Click(object sender, RoutedEventArgs e) => RefreshData();

		private void CancelEditProductButton_Click(object sender, RoutedEventArgs e)
		{
			if (ProductProfile.DataContext is Product product)
				ProductProfile.DataContext = product.Get();
		}

		private void SaveEditedChangesInProductButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidationTextBoxes())
			{
				if (ProductProfile.DataContext is Product product)
				{
					this._confirmWindow.Show($"Potwierdź edycję produktu {this.EditedName.Text}?", out bool isConfirmed, $"Edytuj {this.EditedName.Text}");
					if (isConfirmed)
					{
						Product.EditProduct(product);
					}
					else
					{
						if(product.Get() is Product dbProduct)
                            DataContext = dbProduct.Get();
					}
				}
				else InformationToUser($"{ErrorMesages.DURING_EDIT_PRODUCT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
			}
			ProductListViewModel.Instance.Refresh();
		}

		private void CancelAddingButton_Click(object sender, RoutedEventArgs e) => ClearData();

        #region WarehouseDatagridFilter_TextChanged
        private void WarehouseDatagridFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox filterTextBox = sender as TextBox;
            string searchingText = filterTextBox.Text;
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(ProductsList.ItemsSource);

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
        #endregion
    }
}
