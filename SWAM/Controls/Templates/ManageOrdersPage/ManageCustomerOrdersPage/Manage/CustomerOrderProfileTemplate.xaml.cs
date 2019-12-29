using System.Windows.Controls;
using System.Windows;
using SWAM.Models.Customer;
using SWAM.Enumerators;
using SWAM.Converters;
using System.Linq;
using System;
using SWAM.Windows;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageCustomerOrdersPage.Manage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrderProfileTemplate.xaml
    /// </summary>
    public partial class CustomerOrderProfileTemplate : UserControl
    {
        #region Constructor
        public CustomerOrderProfileTemplate()
        {
            InitializeComponent();
        }
        #endregion

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

                    IsPaid.Text = new ENtoPLpaymentStatus().Convert(EditPaymentStatus.SelectedItem, null, null, null).ToString();

                    customerOrder.Edit(customerOrder);
                    CustomerOrdersListViewModel.Instance.Refresh(customerOrder.Customer);
                }
            }
        }
        #endregion

        private bool ValidateChangeDeliveryStatus(CustomerOrder customerOrder, CustomerOrderStatus selectedItem)
        {
            if (Math.Abs(customerOrder.CustomerOrderStatus - selectedItem) > 1)
                return false;
            return true;
        }

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
                if (!ValidateChangeDeliveryStatus(customerOrder, (CustomerOrderStatus)EditOrderStatus.SelectedItem))
                {
                    EditOrderStatus.SelectedItem = customerOrder.CustomerOrderStatus;
                    new WarningWindow().Show("Można wybrać tylko sąsiadujący status.");
                    return;
                }

                var direction = ((int)customerOrder.CustomerOrderStatus - (int)(CustomerOrderStatus)EditOrderStatus.SelectedItem) == 1
                                ? StatusDirectionChange.Forward : StatusDirectionChange.Backward;

                CustomerOrder.ChangeDeliveryStatus((CustomerOrderStatus)EditOrderStatus.SelectedItem, customerOrder);
                customerOrder.CustomerOrderStatus = (CustomerOrderStatus)EditOrderStatus.SelectedItem;
                CusomterOrderStatus.Text = new ENtoPLcustomerOrderStatus().Convert(EditOrderStatus.SelectedItem, null, null, null).ToString();

                if (direction == StatusDirectionChange.Forward)
                {
                    if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.InDelivery)
                        RemoveProductsFromState(customerOrder);

                    if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.Delivered)
                    {
                        DeliveryDate.Text = DateTime.Now.ToShortDateString();
                    }
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

        #region RemoveProductsFromState
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
        #endregion

        #region AddProductsToState
        private void AddProductsToState(CustomerOrder customerOrder)
        {
            var context = new ApplicationDbContext();

            foreach (var position in customerOrder.CustomerOrderPositions)
            {
                var state = context.States.SingleOrDefault(s => s.Id == position.State.Id);
                state.Quantity += position.Quantity;
                state.Booked += position.Quantity;
                context.SaveChanges();
            }
        }
        #endregion

        #region UserControl_Loaded
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerOrder customerOrder)
            {
                #region Selection EditOrderStatus combobox items
                if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.WaitingForPayment)
                    EditOrderStatus.SelectedItem = CustomerOrderStatus.WaitingForPayment;

                else if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.InProcess)
                    EditOrderStatus.SelectedItem = CustomerOrderStatus.InProcess;

                else if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.InDelivery)
                    EditOrderStatus.SelectedItem = CustomerOrderStatus.InDelivery;

                else if (customerOrder.CustomerOrderStatus == CustomerOrderStatus.Delivered)
                    EditOrderStatus.SelectedItem = CustomerOrderStatus.Delivered;
                #endregion

                #region Selection EditPaymentStatus combobox items
                EditPaymentStatus.SelectedItem = (customerOrder.IsPaid) ? PaymentStatus.Paid : PaymentStatus.AwaitingPayment;
                #endregion

                if (customerOrder.ShipmentType == ShipmentType.Reception)
                {
                    CourierContainer.Visibility = Visibility.Hidden;
                }
            }
        }
        #endregion
    }
}