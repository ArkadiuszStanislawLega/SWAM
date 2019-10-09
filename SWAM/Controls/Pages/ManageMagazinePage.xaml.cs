using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.MagazineListViewModel;
using SWAM.Strings;

namespace SWAM
{
    public partial class ManageMagazinePage : BasicUserControl
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
        public ManageMagazinePage()
        {
            InitializeComponent();
            DataContext = this._stateList;
            _stateList.Refresh();
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
                if (context.Products.FirstOrDefault(p => p.Id == state.Id) != null)
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
                            _context = new ApplicationDbContext();
                            var dbState = context.States.FirstOrDefault(p => p.Id == state.Id);
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









    }






}
