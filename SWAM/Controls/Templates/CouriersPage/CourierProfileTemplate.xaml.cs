using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Enumerators;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
using SWAM.Strings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.CouriersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CourierProfileTemplate.xaml
    /// </summary>
    public partial class CourierProfileTemplate : BasicUserControl
    {
        #region Properties
        /// <summary>
        /// Property to retrieve the proper CustomerOrder class property names. 
        /// </summary>
        private static readonly CustomerOrder _sampleCustomerOrder = new CustomerOrder();
        /// <summary>
        /// Default list sort setting.
        /// </summary>
        private static readonly string _defaultSortValue = nameof(_sampleCustomerOrder.Id);
        /// <summary>
        /// Property according to which it sorts the list of orders.
        /// </summary>
        private string _propertyByWitchIsSort = _defaultSortValue;
        /// <summary>
        /// Dictionary for proper sorting.
        /// </summary>
        private Dictionary<CustomerOrdersListSortingType, string> _propertiesByWhichSortingCanTakePlace = new Dictionary<CustomerOrdersListSortingType, string>()
        {
            { CustomerOrdersListSortingType.Id, nameof(_sampleCustomerOrder.Id) },
            { CustomerOrdersListSortingType.OrderDate, nameof(_sampleCustomerOrder.OrderDate) },
            { CustomerOrdersListSortingType.OrderStatus, nameof(_sampleCustomerOrder.CustomerOrderStatus) }
        };
        #endregion

        #region Basic constructor
        public CourierProfileTemplate()
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
            this.SortByOrderStatus.SelectedValue = CustomerOrderStatus.Delivered;
        }
        #endregion
        #region SetButtonsEvents
        /// <summary>
        /// Sets all edit buttons to press triggers.
        /// </summary>
        private void SetButtonsEvents()
        {
            this.CourierName.ConfirmChangeName.Click += ConfirmChangeName_Click;
            this.CourierPhone.ConfirmChangePhone.Click += ConfirmChangePhone_Click;
            this.CourierTIN.ConfirmChangeTIN.Click += ConfirmChangeTIN_Click;
            this.CourierEmailAddress.ConfirmChangeEmailAddress.Click += ConfirmChangeEmailAddress_Click;
        }
        #endregion
        #region ConfirmChangeName_Click
        /// <summary>
        /// Action after click confirm change name button.
        /// Change name of specific courier in database.
        /// </summary>
        /// <param name="sender">Confirm change name button.</param>
        /// <param name="e">Click confirm change name button</param>
        private void ConfirmChangeName_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Courier courier)
            {
                if (!Courier.IsNameExist(this.CourierName.EditName.Text))
                {
                    courier.Name = this.CourierName.EditName.Text;
                    courier.Edit(courier);
                    DataContext = Courier.Get(courier.Id);
                    CouriersListViewModel.Instance.Refresh();

                    InformationToUser($"Zaktualizowano nazwę kuriera {courier.Name}.");
                }
                else
                    InformationToUser($"Nazwa {this.CourierName.EditName.Text} jest już używana.", true);
            }
            else
                InformationToUser($"{ErrorMesages.DATACONTEXT_ERROR} Wystąpił błąd pod czas dodawania nowego kuriera.", true);
        }
        #endregion
        #region ConfirmChangePhone_Click
        /// <summary>
        /// Action after click confirm change phone button.
        /// Change phone of specific courier in database.
        /// </summary>
        /// <param name="sender">Confirm change phone button.</param>
        /// <param name="e">Click confirm change phone button</param>
        private void ConfirmChangePhone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Courier courier)
            {
                courier.Phone = this.CourierPhone.EditPhone.Text;
                courier.Edit(courier);
                DataContext = Courier.Get(courier.Id);
                CouriersListViewModel.Instance.Refresh();

                InformationToUser($"Zaktualizowano numer telefonu kontaktowego kuriera {courier.Name}.");
            }
        }
        #endregion
        #region ConfirmChangeTIN_Click
        /// <summary>
        /// Action after click confrim change TIN.
        /// Changing TIN value in database.
        /// </summary>
        /// <param name="sender">Confirm change TIN button.</param>
        /// <param name="e">Event clicked.</param>
        private void ConfirmChangeTIN_Click(object sender, RoutedEventArgs e)
        {
            //TODO:Create TIN validation.
            if (this.CourierTIN.EditTin.Text != string.Empty)
            {
                if (DataContext is Courier courier)
                {
                    courier.Tin = this.CourierTIN.EditTin.Text;
                    courier.Edit(courier);
                    DataContext = Courier.Get(courier.Id);
                    CouriersListViewModel.Instance.Refresh();

                    InformationToUser($"Zaktualizowano TIN kuriera {courier.Name}.");
                }
            }
        }
        #endregion
        #region ConfirmChangeEmailAddress_Click
        /// <summary>
        /// Action after click confirm change e-mail address button.
        /// Change e-mail address of specific courier in database.
        /// </summary>
        /// <param name="sender">Confirm change e-mail address button.</param>
        /// <param name="e">Click confirm change e-mail address button</param>
        private void ConfirmChangeEmailAddress_Click(object sender, RoutedEventArgs e)
        {
            if (Models.EmailAddress.IsValidEmail(this.CourierEmailAddress.EditEmailAddress.Text))
            {
                if (DataContext is Courier courier)
                {
                    courier.EmailAddress = this.CourierEmailAddress.EditEmailAddress.Text;
                    courier.Edit(courier);
                    DataContext = Courier.Get(courier.Id);
                    CouriersListViewModel.Instance.Refresh();


                    InformationToUser($"Zaktualizowano adres e-mail kuriera {courier.Name}.");
                }
            }
            else InformationToUser($"Adres e-mail {this.CourierEmailAddress.EditEmailAddress.Text} jest nieprawidłowy.", true);
        }
        #endregion

        #region SortAscending_Click
        /// <summary>
        /// Action after click checkBox in filters container to change type of sorting(ascending/descending) user list.
        /// </summary>
        /// <param name="sender">Descending check box</param>
        /// <param name="e">Event is clicked</param>
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
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(_sampleCustomerOrder.Id), System.ComponentModel.ListSortDirection.Ascending));
                else
                    this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(nameof(_sampleCustomerOrder.Id), System.ComponentModel.ListSortDirection.Descending));
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
        #region ChangeFilterWhenFilteringIsByOrderStatus
        /// <summary>
        /// Changing courier orders list view model filtering by currentyle selected customer order status.
        /// </summary>
        private void ChangeFilterWhenFilteringIsByOrderStatus()
        {
            if (this.SortByOrderStatus.SelectedValue != null)
            {
                var value = (CustomerOrderStatus)this.SortByOrderStatus.SelectedValue;
                ICollectionView filter = CollectionViewSource.GetDefaultView(CourierOrdersListViewModel.Instance.Orders);
                filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhom = customerOrder as CustomerOrder;
                        return allCustomerOrdersWhom.CustomerOrderStatus.ToString().Contains(value.ToString());
                };
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
            ICollectionView filter = CollectionViewSource.GetDefaultView(CourierOrdersListViewModel.Instance.Orders);
            if (filter != null)
            {
                filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhom = customerOrder as CustomerOrder;
                    switch ((CustomerOrdersListSortingType)this.SortBy.SelectedValue)
                    {
                        case CustomerOrdersListSortingType.Id:
                            return allCustomerOrdersWhom.Id.ToString().Contains(this.OrderNumberInput.Text);
                        case CustomerOrdersListSortingType.OrderDate:
                            return allCustomerOrdersWhom.OrderDate.ToString().Contains(this.OrderNumberInput.Text);
                        case CustomerOrdersListSortingType.OrderStatus:
                            return allCustomerOrdersWhom.CustomerOrderStatus.ToString().Contains(this.OrderNumberInput.Text);
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
