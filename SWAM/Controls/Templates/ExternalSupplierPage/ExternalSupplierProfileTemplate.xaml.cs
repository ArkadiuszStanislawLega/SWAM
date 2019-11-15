using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Enumerators;
using SWAM.Models.ExternalSupplier;
using SWAM.Models.Warehouse;
using SWAM.Strings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSupplierProfileTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierProfileTemplate : BasicUserControl
    {
        #region Properties
        /// <summary>
        /// Property to retrieve the proper WarehouseOrder class property names. 
        /// </summary>
        private static readonly WarehouseOrder _sampleWarehouseOrder = new WarehouseOrder();
        /// <summary>
        /// Default list sort setting.
        /// </summary>
        private static readonly string _defaultSortValue = nameof(_sampleWarehouseOrder.Id);
        /// <summary>
        /// Property according to which it sorts the list of orders.
        /// </summary>
        private string _propertyByWitchIsSort = _defaultSortValue;
        /// <summary>
        /// Dictionary for proper sorting.
        /// </summary>
        private Dictionary<CustomerOrdersListSortingType, string> _propertiesByWhichSortingCanTakePlace = new Dictionary<CustomerOrdersListSortingType, string>()
        {
            { CustomerOrdersListSortingType.Id, nameof(_sampleWarehouseOrder.Id) },
            { CustomerOrdersListSortingType.OrderDate, nameof(_sampleWarehouseOrder.OrderDate) },
            { CustomerOrdersListSortingType.OrderStatus, nameof(_sampleWarehouseOrder.WarehouseOrderStatus) }
        };
        #endregion
        #region Deafault Constructor
        public ExternalSupplierProfileTemplate()
        {
            InitializeComponent();

            SetButtonsEvents();
            SetDefaultValuesInComboBoxes();
        }
        #endregion

        #region SetDefaultValuesInComboBoxes
        /// <summary>
        /// Setting default values in combo boxes.
        /// </summary>
        private void SetDefaultValuesInComboBoxes()
        {
            this.SortBy.SelectedValue = CustomerOrdersListSortingType.Id;
            this.SortByOrderStatus.SelectedValue = WarehouseOrderStatus.Delivered;
        }
        #endregion
        #region SetButtonsEvents
        /// <summary>
        /// Sets all edit buttons to press triggers.
        /// </summary>
        private void SetButtonsEvents()
        {
            this.ProperName.ConfirmChangeProperName.Click += ConfirmChangeProperName_Click;
            this.Tin.ConfirmChangeTIN.Click += ConfirmChangeTIN_Click;
            this.AdressEmail.ConfirmChangeEmailAddress.Click += ConfirmChangeEmailAddress_Click;
        }
        #endregion

        #region ConfirmChangeProperName_Click
        /// <summary>
        /// Action after click confirm change proper name button.
        /// Update proper name of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change proper name button.</param>
        /// <param name="e">Event click change proper name button.</param>
        private void ConfirmChangeProperName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.Edit(new ExternalSupplier()
                {
                    Id = externalSupplier.Id,
                    Name = this.ProperName.EditProperName.Text,
                    Tin = externalSupplier.Tin,
                    Address = externalSupplier.Address
                });
                DataContext = ExternalSupplier.Get(externalSupplier.Id);
                ExternalSupplierListViewModel.Instance.Refresh();

                InformationToUser($"Edytowano nazwę własną zewnętrzengo dostawcy {this.ProperName.EditProperName.Text}.");
            }
            else
                InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PROPER_NAME_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ConfirmChangeTIN_Click
        /// <summary>
        /// Action after click confirm change TIN button.
        /// Update TIN of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change TIN button.</param>
        /// <param name="e">Event click change TIN button.</param>
        private void ConfirmChangeTIN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.Edit(new ExternalSupplier()
                {
                    Id = externalSupplier.Id,
                    Name = externalSupplier.Name,
                    Tin = this.Tin.EditTin.Text,
                    Address = externalSupplier.Address
                });
                DataContext = ExternalSupplier.Get(externalSupplier.Id);

                this.Tin.EditTin.Text = string.Empty;
                InformationToUser($"Edytowano TIN zewnętrzego dostawcy {externalSupplier.Name}.");
            }
            else
                InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_TIN_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ConfirmEditResidentalAddress_Click
        /// <summary>
        /// Action after click confirm change residenta address button.
        /// Update residental address of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change residental address button.</param>
        /// <param name="e">Event click change residental address button.</param>
        private void ConfirmEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                if (this.ResidentalAddress.GetAddress<ExternalSupplierAddress>() is ExternalSupplierAddress editedAddress)
                {
                    externalSupplier.Address.Edit(editedAddress);
                    DataContext = ExternalSupplier.Get(externalSupplier.Id);
                    InformationToUser($"Edytowano adres zewnętrznego dostawcy.");
                }
                else InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);

            this.ResidentalAddress.HideEditControls();
        }
        #endregion
        #region CancelEditResidentalAddress_Click
        /// <summary>
        /// Action after click cancel edit residental address button.
        /// Clear values hide edit controls.
        /// </summary>
        /// <param name="sender">Cancel change residental address button.</param>
        /// <param name="e">Event click cancel change residental address button.</param>
        private void CancelEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResidentalAddress.HideEditControls();
            if (DataContext is ExternalSupplier externalSupplier)
            {
                this.ResidentalAddress.DataContext = ExternalSupplier.Get(externalSupplier.Id).Address;
            }
            else InformationToUser($"{ErrorMesages.CANCEL_EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ChangeResidentalAddress_Click
        /// <summary>
        /// Action after click change residental address button.
        /// Show edit residental address controls.
        /// </summary>
        /// <param name="sender">Button change residental address.</param>
        /// <param name="e">Event click button change residental address.</param>
        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) 
            => this.ResidentalAddress.ShowEditControls();
        #endregion
        #region ConfirmChangeEmailAddress_Click
        /// <summary>
        /// Action after click confirm edit email address button.
        /// </summary>
        /// <param name="sender">Confirm edit email address button</param>
        /// <param name="e">Event clicked.</param>
        private void ConfirmChangeEmailAddress_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                if (Models.EmailAddress.IsValidEmail(this.AdressEmail.EditEmailAddress.Text))
                {
                    externalSupplier.EditEmail(new ExternalSupplierEmailAddress()
                    {
                        EmailAddress = this.AdressEmail.EditEmailAddress.Text
                    });

                    DataContext = ExternalSupplier.Get(externalSupplier.Id);
                    ExternalSupplierListViewModel.Instance.Refresh();

                    InformationToUser($"Edytowano adres email zewnętrzengo dostawcy {externalSupplier.Name}.");
                }
                else InformationToUser($"Wprowadzony adres e-mail, jest nieprawidłowy.", true);
            }
            else
                InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region SortAscending_Click
        /// <summary>
        /// Action after click checkBox in filters container to change type of sorting(ascending/descending) user list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (this.OrdersList.Items.SortDescriptions.Count > 0)
                this.OrdersList.Items.SortDescriptions.RemoveAt(this.OrdersList.Items.SortDescriptions.Count - 1);

            //When sorting is by order status.
            if ((CustomerOrdersListSortingType)this.SortBy.SelectedValue == CustomerOrdersListSortingType.OrderStatus)
            {
                //Changing filtering to currently selected value in combo box - SortByOrderStatus.
                ChangeFilterWhenFilteringIsByOrderStatus();

                //Sort all orders with specific status by order Id ascending or desceding
                if (this.AscendingSorting.IsChecked != true)
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(_sampleWarehouseOrder.Id), System.ComponentModel.ListSortDirection.Ascending));
                else
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(_sampleWarehouseOrder.Id), System.ComponentModel.ListSortDirection.Descending));
            }
            else
            {

                if (this.AscendingSorting.IsChecked != true)
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Ascending));
                else
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Descending));
            }
        }
        #endregion
        #region TextBox_TextChanged
        /// <summary>
        /// Filtering list depends on text typed in TextBox named FindUser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollectionView filter = CollectionViewSource.GetDefaultView(ExternalSupplierDeliveryListViewModel.Instance.WarehouseOrders);
            if (filter != null)
            {
                filter.Filter = customerOrder =>
                {
                    WarehouseOrder allCustomerOrdersWhom = customerOrder as WarehouseOrder;
                    switch ((CustomerOrdersListSortingType)SortBy.SelectedValue)
                    {
                        case CustomerOrdersListSortingType.Id:
                            return allCustomerOrdersWhom.Id.ToString().Contains(this.OrderNumberInput.Text);
                        case CustomerOrdersListSortingType.OrderDate:
                            return allCustomerOrdersWhom.OrderDate.ToString().Contains(this.OrderNumberInput.Text);
                        case CustomerOrdersListSortingType.OrderStatus:
                            return allCustomerOrdersWhom.WarehouseOrderStatus.ToString().Contains(this.OrderNumberInput.Text);
                        default: return false;
                    }
                };
            }
        }
        #endregion
        #region ConfirmSortButton_Click
        /// <summary>
        /// Action after clicked confirm sort button.
        /// Sorts the list of customer orders depending on the settings entered by the user.
        /// </summary>
        /// <param name="sender">Confrim sort button.</param>
        /// <param name="e">Event clicked confrim sort button.</param>
        private void ConfirmSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (this._propertiesByWhichSortingCanTakePlace.TryGetValue((CustomerOrdersListSortingType)this.SortBy.SelectedValue, out this._propertyByWitchIsSort))
            {
                if ((CustomerOrdersListSortingType)this.SortBy.SelectedValue == CustomerOrdersListSortingType.OrderStatus)
                {
                    this.OrderNumberInput.Visibility = Visibility.Collapsed;
                    this.SortByOrderStatus.Visibility = Visibility.Visible;
                }
                else
                {
                    this.OrderNumberInput.Visibility = Visibility.Visible;
                    this.SortByOrderStatus.Visibility = Visibility.Collapsed;
                }
                SortAscending_Click(sender, e);
            }
        }
        #endregion

        #region ChangeFilterWhenFilteringIsByOrderStatus
        /// <summary>
        /// Changing external supplier orders list view model filtering by currentyle selected warehouse order status.
        /// </summary>
        private void ChangeFilterWhenFilteringIsByOrderStatus()
        {
            if (this.SortByOrderStatus.SelectedValue != null)
            {
                var value = (WarehouseOrderStatus)this.SortByOrderStatus.SelectedValue;
                ICollectionView filter = CollectionViewSource.GetDefaultView(ExternalSupplierDeliveryListViewModel.Instance.WarehouseOrders);
                filter.Filter = customerOrder =>
                {
                    WarehouseOrder allCustomerOrdersWhom = customerOrder as WarehouseOrder;
                    return allCustomerOrdersWhom.WarehouseOrderStatus.ToString().Contains(value.ToString());
                };
            }
        }
        #endregion
        #region SortByOrderStatus_SelectionChanged
        /// <summary>
        /// Action after select value from SortByOrderStatus combo box. 
        /// </summary>
        /// <param name="sender">SortByOrderStatus ComboBox</param>
        /// <param name="e">Selection is change.</param>
        private void SortByOrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e) => ChangeFilterWhenFilteringIsByOrderStatus();
        #endregion
    }
}
