using SWAM.Models.AdministratorPage;
using SWAM.Templates.AdministratorPage;
using System.Windows;
using SWAM.Strings;
using SWAM.Models.Warehouse;

namespace SWAM.Controls.Templates.AdministratorPage.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseProfileTemplate.xaml
    /// </summary>
    public partial class WarehouseProfileTemplate : BasicUserControl
    {
        public WarehouseProfileTemplate()
        {
            InitializeComponent();
        }
        #region WarehouseProfileTemplate_Loaded
        private void WarehouseProfileTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is Warehouse context)
            {
                foreach (Warehouse currentWarehouse in WarehousesListViewModel.Instance.WarehousesList)
                {
                    if (currentWarehouse.Id == context.Id)
                    {
                        DataContext = currentWarehouse;
                        break;
                    }
                }
            }
        }
        #endregion
        #region Edit_Click
        /// <summary>
        /// Action after click Edit button.
        /// Show all editable fields.
        /// </summary>
        /// <param name="sender">Edit button.</param>
        /// <param name="e">Event click edit button.</param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Address.ShowEditControls();
            this.WarehouseTechnicalDate.ShowEditControls();
        }
        #endregion
        #region Delete_Click
        /// <summary>
        /// Action after click delete warehouse button.
        /// </summary>
        /// <param name="sender">Delete button</param>
        /// <param name="e">Action button clicked</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string correctDeleteWarehouseMessage;

            if (DataContext is Warehouse data)
            {
                this.ConfirmWindow.Show($"Czy na pewno chcesz usunąć magazyn {data.Name}?", out bool isConfirmed, "Potwierdź usunięcie magazynu");
                if(isConfirmed)
                {
                    if (data.Remove())
                    {
                        correctDeleteWarehouseMessage = $"Magazyn { data.Name} został usunięty.";
                        InformationToUser(correctDeleteWarehouseMessage);
                        //Refresh List with warehouses
                        if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                            template.RefreshList();
                        else InformationToUser($"{correctDeleteWarehouseMessage} {ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR} {ErrorMesages.REFRESH_WAREHOUSES_LIST_ERROR}", true);
                    }
                    else InformationToUser($"{ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR}", true);
                }
            }
            else InformationToUser($"{ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region Confirm_Click
        /// <summary>
        /// Action after click confrim edited changes button.
        /// Checks if all of the given data meet the required conditions, if all the conditions are met, it edits the warehouse properties.
        /// </summary>
        /// <param name="sender">Confirmation button.</param>
        /// <param name="e">Event clicked confirmation button.</param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Warehouse warehouse)
            {
                this.ConfirmWindow.Show($"Czy napewno chcesz zachować zmiany magazynu {warehouse.Name}?", out bool isConfirmed, "Edytowanie włściwości magazynu");
                if (isConfirmed && NameValidation())
                {
                    if (long.TryParse(this.WarehouseTechnicalDate.EditHeight.Text, out long eHeight))
                    {
                        if (long.TryParse(this.WarehouseTechnicalDate.EditWidth.Text, out long eWidth))
                        {
                            if (long.TryParse(this.WarehouseTechnicalDate.EditLength.Text, out long eLenght))
                            {
                                if (long.TryParse(this.WarehouseTechnicalDate.EditAcceptableWeight.Text, out long eAcceptableWeight))
                                {
                                    if (this.Address.GetAddress<WarehouseAddress>() is WarehouseAddress warehouseAddress)
                                    {
                                        warehouse.Edit(new Warehouse()
                                        {
                                            Id = warehouse.Id,
                                            Name = this.WarehouseName.Text,
                                            Height = eHeight,
                                            Width = eWidth,
                                            Length = eLenght,
                                            AcceptableWeight = eAcceptableWeight,
                                            WarehouseAddress = warehouseAddress
                                        });

                                        this.Address.HideEditControls();
                                        this.WarehouseTechnicalDate.HideEditControls();

                                        WarehousesListViewModel.Instance.Refresh();

                                        InformationToUser($"Edytowano magazyn {this.WarehouseName.Text}.");
                                    }
                                    else
                                        InformationToUser("Błędny adres magazynu.", true);
                                }
                                else
                                    InformationToUser("Błędna wartość pole powierzchni netto magazynu.", true);
                            }
                            else
                                InformationToUser("Błędna wartość pole długość magazynu.", true);
                        }
                        else
                            InformationToUser("Błędna wartość pole szerokość magazynu.", true);
                    }
                    else
                        InformationToUser("Błędna wartość pole wysokość magazynu.", true);
                }
                else
                    InformationToUser("Magazyn o takiej nazwie już istnieje.", true);
            }
            else
                InformationToUser($"{ErrorMesages.DURGIN_EDIT_WAREHOUSE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region NameValidation
        /// <summary>
        /// Checks the name of the warehouse whether it complies with accepted standards and whether sometimes the name is already taken.
        /// </summary>
        /// <returns>True if it meets all the conditions.</returns>
        private bool NameValidation()
        {
            if (DataContext is Warehouse warehouse)
            {
                if (this.EditName.Text.Length > SWAM.MainWindow.MIN_NAME_LENGTH && warehouse.Get().Name != this.EditName.Text)
                    if (!Warehouse.IsWarehouseNameExist(this.EditName.Text))
                        return true;
            }

            return true;
        }
        #endregion
    }
}
