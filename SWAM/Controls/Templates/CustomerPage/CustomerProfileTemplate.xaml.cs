using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Enumerators;
using SWAM.Models.Customer;
using SWAM.Strings;
using System.Collections.Generic;
using System.Windows;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfileTemplate : BasicUserControl
    {
        private static readonly CustomerOrder _sampleCustomerOrder = new CustomerOrder();
        /// <summary>
        /// Property according to which it sorts the list of orders.
        /// </summary>
        private string _propertyByWitchIsSort = string.Empty;

        private Dictionary<CustomerOrdersListSortingType, string> _propertiesByWhichSortingCanTakePlace = new Dictionary<CustomerOrdersListSortingType, string>()
        {
            { CustomerOrdersListSortingType.Id, nameof(_sampleCustomerOrder.Id) }, 
            { CustomerOrdersListSortingType.OrderDate, nameof(_sampleCustomerOrder.OrderDate) },
            { CustomerOrdersListSortingType.OrderStatus, nameof(_sampleCustomerOrder.CustomerOrderStatus) } 
        };

        public CustomerProfileTemplate()
        {
            InitializeComponent();

            this.Name.ConfirmChangeName.Click += ConfirmChangeName_Click;
            this.Surname.ConfirmChangeSurname.Click += ConfirmChangeSurname_Click;
            this.Phone.ConfirmChangePhone.Click += ConfirmChangePhone_Click;
            this.EmailAddress.ConfirmChangeEmailAddress.Click += ConfirmChangeEmailAddress_Click;
        }
        private void ConfirmChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeSurname_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangePhone_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) => ResidentalAddress.ShowEditControls();

        private void StackPanel_ManipulationInertiaStarting(object sender, System.Windows.Input.ManipulationInertiaStartingEventArgs e)
        {

        }



        #region SortAscending_Click
        /// <summary>
        /// Action after click checkBox in filters container to change type of sorting(ascending/descending) user list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            //Delete the last setting
            if (OrdersList.Items.SortDescriptions.Count > 0)
                OrdersList.Items.SortDescriptions.RemoveAt(OrdersList.Items.SortDescriptions.Count - 1);

            if (AscendingSorting.IsChecked == true)
                OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Ascending));
            else
                OrdersList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(this._propertyByWitchIsSort, System.ComponentModel.ListSortDirection.Descending));
        }
        #endregion

        #region ConfirmSortButton_Click
        private void ConfirmSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (this._propertiesByWhichSortingCanTakePlace.TryGetValue((CustomerOrdersListSortingType)SortBy.SelectedValue, out _propertyByWitchIsSort))
            {
                SortAscending_Click(sender, e);
            }
        }
        #endregion
    }
}
