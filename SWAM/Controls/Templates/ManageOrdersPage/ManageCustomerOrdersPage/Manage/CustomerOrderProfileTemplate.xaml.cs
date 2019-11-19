using System.Windows.Controls;
using SWAM.Models.ManageOrdersPage;
using System.Windows;
using SWAM.Strings;
using SWAM.Models.User;

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
           if (DataContext is CustomerOrdersListViewModel CustomerOrderStatus)
            {      
                var customerOrderStatus = (Enumerators.CustomerOrderStatus)this.EditOrderStatus.SelectedValue;

                CustomerOrderStatus.ChangeStatus(customerOrderStatus);

                CustomerOrdersListViewModel.Instance.Refresh();

                this.EditOrderStatus.SelectedValue = null;
            }
        }
        #endregion

    }






















}
