using System.Windows.Controls;
using System.Windows;
using SWAM.Models.Customer;
using SWAM.Enumerators;
using SWAM.Converters;
using System.Linq;
using System;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.Manage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrderProfileTemplate.xaml
    /// </summary>
    public partial class CustomerOrderProfileTemplate : UserControl
    {
        public CustomerOrderProfileTemplate()
        {
            InitializeComponent();
        }

        #region ConfirmEditPaymentStatus_Click
        /// <summary>
        /// Action after click confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmEditStatus_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerOrder customerOrder)
            {
                if (this.EditPaymentStatus.SelectedItem != null)
                {
                    if ((PaymentStatus)this.EditPaymentStatus.SelectedItem == PaymentStatus.Paid)
                        customerOrder.IsPaid = true;
                    else
                        customerOrder.IsPaid = false;

                    customerOrder.Edit(customerOrder);
                    this.DataContext = CustomerOrder.Get(customerOrder.Id);

                    CustomerOrdersListViewModel.Instance.Refresh(customerOrder.Customer);

                    this.EditOrderStatus.SelectedValue = null;
                }
            }
        }
        #endregion

        #region ConfirmEditDeliverytStatus_Click
        /// <summary>
        /// Action after click confirm change permision button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmEditDeliveryStatus_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerOrder customerOrder
                && EditOrderStatus.SelectedItem != null
                && customerOrder.CustomerOrderStatus != (CustomerOrderStatus)EditOrderStatus.SelectedItem)
            {
                CustomerOrder.ChangeDeliveryStatus((CustomerOrderStatus)EditOrderStatus.SelectedItem, customerOrder);
                customerOrder.CustomerOrderStatus = (CustomerOrderStatus)EditOrderStatus.SelectedItem;
                CusomterOrderStatus.Text = new ENtoPLcustomerOrderStatus().Convert(EditOrderStatus.SelectedItem, null, null, null).ToString();

                if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.InDelivery)
                    RemoveProductsFromState(customerOrder);

                if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.Delivered)
                {
                    DeliveryDate.Text = DateTime.Now.ToShortDateString();
                }
            }
        }
        #endregion

        #region ChangeDeliveryDate_Click
        /// <summary>
        /// Action when change user expire date is loaded.
        /// Set new data context.
        /// </summary>
        /// <param name="sender">Change user expire date.</param>
        /// <param name="e">Evenet is loaded.</param>
        private void ChangeDeliveryDate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is CustomerOrder customerOrder && EditOrderStatus.SelectedItem != null)
            {
                CustomerOrder.ChangeDateOfDelivery(Calendar.SelectedDate, customerOrder);
                DataContext = new ApplicationDbContext();
                DataContext = customerOrder;
            }
        }
        #endregion

        private void RemoveProductsFromState(CustomerOrder customerOrder)
        {
            var context = new ApplicationDbContext();

            foreach (var position in customerOrder.CustomerOrderPositions)
            {
                var state = context.States.SingleOrDefault(s => s.Id == position.State.Id);
                state.Quantity -= position.Quantity;
                state.Booked -= position.Quantity;
                context.SaveChanges();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerOrder customerOrder)
            {
                if (customerOrder.ShipmentType == ShipmentType.Reception)
                {
                    this.CourierContainer.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
