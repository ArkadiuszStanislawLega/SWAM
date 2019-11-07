using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfileTemplate : BasicUserControl
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
        public CustomerProfileTemplate()
        {
            InitializeComponent();

            SetButtonsEvents();
        }
        #endregion

        #region SetButtonsEvents
        /// <summary>
        /// Sets all edit buttons to press triggers.
        /// </summary>
        private void SetButtonsEvents()
        {
            this.CustomerName.ConfirmChangeName.Click += ConfirmChangeName_Click;
            this.CustomerSurname.ConfirmChangeSurname.Click += ConfirmChangeSurname_Click;
            this.CustomerPhone.ConfirmChangePhone.Click += ConfirmChangePhone_Click;
            this.CustomerEmailAddress.ConfirmChangeEmailAddress.Click += ConfirmChangeEmailAddress_Click;
        }
        #endregion
        #region ConfirmChangeName_Click
        /// <summary>
        /// Action after click confirm change name button.
        /// Change name of specific customer in database.
        /// </summary>
        /// <param name="sender">Confirm change name button.</param>
        /// <param name="e">Click confirm change name button</param>
        private void ConfirmChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(DataContext is Customer customer)
            {
                customer.Name = CustomerName.EditName.Text;
                customer.Edit(customer);
                DataContext = Customer.Get(customer.Id);
                CustomersListViewModel.Instance.Refresh();

                InformationToUser($"Zaktualizowano imię klienta {customer.Name} {customer.Surname}.");
            }
        }
        #endregion
        #region ConfirmChangeSurname_Click
        /// <summary>
        /// Action after click confirm change surname button.
        /// Change surname of specific customer in database.
        /// </summary>
        /// <param name="sender">Confirm change surname button.</param>
        /// <param name="e">Click confirm change surname button</param>
        private void ConfirmChangeSurname_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is Customer customer)
            {
                customer.Surname = CustomerSurname.EditSurname.Text;
                customer.Edit(customer);
                DataContext = Customer.Get(customer.Id);
                CustomersListViewModel.Instance.Refresh();

                InformationToUser($"Zaktualizowano nazwisko klienta {customer.Name} {customer.Surname}.");
            }
        }
        #endregion
        #region ConfirmChangePhone_Click
        /// <summary>
        /// Action after click confirm change phone button.
        /// Change phone of specific customer in database.
        /// </summary>
        /// <param name="sender">Confirm change phone button.</param>
        /// <param name="e">Click confirm change phone button</param>
        private void ConfirmChangePhone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is Customer customer)
            {
                customer.Phone = CustomerPhone.EditPhone.Text;
                customer.Edit(customer);
                DataContext = Customer.Get(customer.Id);
                CustomersListViewModel.Instance.Refresh();

                InformationToUser($"Zaktualizowano numer kontaktowy klienta {customer.Name} {customer.Surname}.");
            }
        }
        #endregion
        #region ConfirmChangeEmailAddress_Click
        /// <summary>
        /// Action after click confirm change email address button.
        /// Change email address of specific customer in database.
        /// </summary>
        /// <param name="sender">Confirm change email address button.</param>
        /// <param name="e">Click confirm change email address button</param>
        private void ConfirmChangeEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is Customer customer)
            {
                if (Models.EmailAddress.IsValidEmail(this.CustomerEmailAddress.EditEmailAddress.Text))
                {
                    customer.EmailAddress = this.CustomerEmailAddress.EditEmailAddress.Text;
                    customer.Edit(customer);
                    DataContext = Customer.Get(customer.Id);
                    CustomersListViewModel.Instance.Refresh();

                    InformationToUser($"Zaktualizowano adres e-mail klienta {customer.Name} {customer.Surname}.");
                }
                else
                    InformationToUser($"Adres e-email {this.CustomerEmailAddress.EditEmailAddress.Text} jest błędny.", true);
            }
        }
        #endregion
        #region ConfirmEditResidentalAddress_Click
        /// <summary>
        /// Action after click confirm change residental address button.
        /// Change residental address of specific customer in database.
        /// </summary>
        /// <param name="sender">Confirm change residental address button.</param>
        /// <param name="e">Click confirm change residental address button</param>
        private void ConfirmEditResidentalAddress_Click(object sender, RoutedEventArgs e)
        {
            this.ResidentalAddress.HideEditControls();

            if (DataContext is Customer customer)
            {
                customer.ResidentalAddress.Edit(this.ResidentalAddress.GetAddress<CustomerResidentalAddress>());
                DataContext = Customer.Get(customer.Id);
                CustomersListViewModel.Instance.Refresh();

                InformationToUser($"Zaktualizowano adres zamieszkania klienta {customer.Name} {customer.Surname}.");
            }
        }
        #endregion
        #region CancelEditResidentalAddress_Click
        /// <summary>
        /// Action after click cancel edit residental address button.
        /// </summary>
        /// <param name="sender">Cancel edit residental address button.</param>
        /// <param name="e">Cancel edit residental address button</param>
        private void CancelEditResidentalAddress_Click(object sender, RoutedEventArgs e)
        {
            this.ResidentalAddress.HideEditControls();

            if (DataContext is Customer customer)
            {
                this.DataContext = Customer.Get(customer.Id);
            }
        }
        #endregion
        #region ChangeResidentalAddress_Click
        /// <summary>
        /// Action after click change residental address button.
        /// Change residental address tempalate to edit mode.
        /// </summary>
        /// <param name="sender">Button change residental address button.</param>
        /// <param name="e">Event click change residental address button.</param>
        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) => this.ResidentalAddress.ShowEditControls();
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

            if (this.AscendingSorting.IsChecked != true)
                this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Ascending));
            else
                this.OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Descending));
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
            //TODO: Debug this. 
            ICollectionView filter = CollectionViewSource.GetDefaultView(CustomerOrdersListViewModel.Instance);
            if (filter != null)
            {
                filter.Filter = customerOrder =>
                {
                    CustomerOrder allCustomerOrdersWhom = customerOrder as CustomerOrder;
                    switch ((CustomerOrdersListSortingType)SortBy.SelectedValue)
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
                SortAscending_Click(sender, e);
            }
        }
        #endregion
    }
}
