using System.Windows.Controls;
using System.Windows;
using SWAM.Models.Customer;
using SWAM.Enumerators;

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

        #region ConfirmEditStatus_Click
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

    }






















}
