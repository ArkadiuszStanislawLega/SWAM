using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SWAM.Controls.Pages;
using SWAM.Models;
using SWAM.Models.ViewModels.ManageMagazinePage;
using SWAM.Models.Warehouse;
using SWAM.Strings;

namespace SWAM
{
    public partial class ManageMagazinePage : BasicPage
    {
        #region Properties
        private int _quantity;
        private int _selectedWarehouse;
        private int _selectedProduct;
        #endregion
        #region BasicConstructor
        public ManageMagazinePage()
        {
            InitializeComponent();
        }
        #endregion

        #region MagazineList_LeftMouseButtonDown
        /// <summary>
        /// Action after click item in product list.
        /// </summary>
        /// <param name="sender">Item from ProductListViewModel</param>
        /// <param name="e">Mouse button click</param>
        private void MagazineList_LeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ProductProfile.DataContext = this.ProductsList.SelectedItem;
            this._selectedProduct = this.ProductsList.SelectedIndex;
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
            if (ProductProfile.DataContext is State state)
            {
                this.EditedQuantity.Text = $"{state.Quantity}";
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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationTextBoxes())
            {
                if (ProductProfile.DataContext is State state)
                {
                    this._confirmWindow.Show($"Potwierdź edycję zasobu {state.Product.Name}?", out bool isConfirmed, $"Edytuj {state.Product.Name}");
                    if (isConfirmed)
                    {
                        if (int.TryParse(this.EditedQuantity.Text, out int editedQuantity))
                        {
                            state.Quantity = editedQuantity;
                            State.EditState(state);
                            StatesViewModel.Instance.Refresh();
                            RefreshDataInProductProfile(this._selectedWarehouse, this._selectedProduct);
                        }
                    }
                }
                else InformationToUser($"{ErrorMesages.DURING_EDIT_PRODUCT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
            }
        }
        #endregion
        #region ValidationTextBoxes
        /// <summary>
        /// Validate fields that should be filled
        /// </summary>
        /// <returns>True if all numbers are correct.</returns>
        bool ValidationTextBoxes()
        {
            if (int.TryParse(this.EditedQuantity.Text, out this._quantity) && this._quantity > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region RefreshDataInProductProfile
        /// <summary>
        /// Refreshing data in product profile same as click on items in lists.
        /// </summary>
        /// <param name="warehouseListIndex">Index from the list of warehouses.</param>
        /// <param name="productIndex">Index from the list of products.</param>
        private void RefreshDataInProductProfile(int warehouseListIndex = 0, int productIndex = 0)
        {
            // Select all first buttons in current view.
            if (this.warehouseListView.Items.Count > 0 && warehouseListIndex < this.warehouseListView.Items.Count)
            {
                this.warehouseListView.SelectedIndex = warehouseListIndex;
                this.ProductsList.ItemsSource = StatesViewModel.Instance.States;
                WarehouseListViewItem_PreviewMouseLeftButtonUp(this.warehouseListView, new RoutedEventArgs());

                if (StatesViewModel.Instance.States.Count > 0 && productIndex < StatesViewModel.Instance.States.Count)
                {
                    this.ProductsList.SelectedIndex = productIndex;
                    this.ProductProfile.DataContext = this.ProductsList.SelectedItem;
                }
            }
        }
        #endregion
        #region Window_Loaded
        /// <summary>
        /// Provide initial configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // obtain a reference to the CollectionView instance
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(warehouseListView.ItemsSource);
            // assign a delegate to the Filter property
            view.Filter = WarehouseFilter;

            // Select all first buttons in current view.
            RefreshDataInProductProfile();
        }
        #endregion

        #region WarehouseFilter
        /// <summary>
        /// Check if warehouse name contains searching text
        /// </summary>
        /// <param name="item">customer object</param>
        /// <returns>True if does</returns>
        private bool WarehouseFilter(object item)
        {
            if (item is Warehouse warehouse)
            {
                return (warehouse.Name.IndexOf(warehouseFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        #endregion

        #region WarehouseFilter_TextChanged
        /// <summary>
        /// Recreate warehouse list view when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(warehouseListView.ItemsSource).Refresh();
        }
        #endregion

        #region WarehouseListViewItem_PreviewMouseLeftButtonUp
        /// <summary>
        /// Fill parent (CreateNewWarehouseOrderTemplate) control DataContext with clicked warehouse data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarehouseListViewItem_PreviewMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            if (sender is ListView warehousesList)
            {
                this._selectedWarehouse = warehousesList.SelectedIndex;
                if (warehousesList.SelectedItem is Warehouse warehouse)
                {
                    if (warehouse == null)
                        return;

                    StatesViewModel.Instance.SetStates(warehouse);
                }
            }
        }
        #endregion

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
                    State state = @object as State;
                    return (state.Product.Name.ToUpper().StartsWith(searchingText.ToUpper()));
                };
            }
        }
        #endregion
    }
}