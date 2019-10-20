using SWAM.Enumerators;
using SWAM.Models.Courier;
using SWAM.Models.Customer;
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
    public partial class CourierProfileTemplate : UserControl
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

        public CourierProfileTemplate()
        {
            InitializeComponent();
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
            ICollectionView filter = CollectionViewSource.GetDefaultView(CourierOrdersListViewModel.Instance);
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
