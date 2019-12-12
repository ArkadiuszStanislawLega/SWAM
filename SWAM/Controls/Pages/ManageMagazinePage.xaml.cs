using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SWAM.Controls.Pages;
using SWAM.Models;
using SWAM.Models.MagazineListViewModel;
using SWAM.Models.ViewModels.ManageMagazinePage;
using SWAM.Models.Warehouse;
using SWAM.Strings;

namespace SWAM
{
    public partial class ManageMagazinePage : BasicPage
    {
        enum Operation { none, edit, add };

        #region Properties

        Operation _currentOperation;
 
        private string _product;

        private string _warehouse;

        private int _available;

        private int _quantity;

        private MagazineListViewModel _stateList = new MagazineListViewModel();

        private ApplicationDbContext _context = new ApplicationDbContext();

        public ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return _context;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => _context = value;
        }
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
            EditButton.IsEnabled = true;
            SaveButton.IsEnabled = false;
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
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductProfile.DataContext is State state)
            {
                if (Context.Products.FirstOrDefault(p => p.Id == state.Id) != null)
                {
                    this._currentOperation = Operation.edit;
                    SaveButton.IsEnabled = true;
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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationTextBoxes())
            {
                 if (this._currentOperation == Operation.edit)
                {
                    if (ProductProfile.DataContext is State state)
                    {
                        this._confirmWindow.Show($"Potwierdź edycję zasobu {this.EditedName.Text}?", out bool isConfirmed, $"Edytuj {this.EditedName.Text}");
                        if (isConfirmed)
                        {
                            State.EditState(state);
                        }
                        else
                        {
                            Context = new ApplicationDbContext();
                            var dbState = Context.States.FirstOrDefault(p => p.Id == state.Id);
                            state.Quantity = dbState.Quantity;
                        }
                    }
                    else InformationToUser($"{ErrorMesages.DURING_EDIT_PRODUCT_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
                }
                this._stateList.Refresh();
                this._currentOperation = Operation.none;
            }

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
            var item = (sender as ListView).SelectedItem;
            if ((sender as ListView).SelectedItem is Warehouse warehouse)
            {
                if (warehouse == null)
                    return;

                StatesViewModel.Instance.SetStates(warehouse);
            }
        }
        #endregion

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}