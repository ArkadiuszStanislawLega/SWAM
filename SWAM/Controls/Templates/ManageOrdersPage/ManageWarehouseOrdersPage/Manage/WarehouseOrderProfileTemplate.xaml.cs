using SWAM.Enumerators;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SWAM.Models.Warehouse;
using System.Text.RegularExpressions;
using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses;


namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.Manage
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseOrderProfileTemplate.xaml
    /// </summary>
    public partial class WarehouseOrderProfileTemplate : BasicUserControl
    {
        public WarehouseOrderProfileTemplate()
        {		
			InitializeComponent();			
		}

        private void ConfirmStatusChange_Button(object sender, RoutedEventArgs e)
        {			
			if (DataContext is WarehouseOrder warehouseOrder &&
                EditOrderStatus.SelectedItem != null &&
				warehouseOrder.WarehouseOrderStatus != (WarehouseOrderStatus)EditOrderStatus.SelectedItem)
            {
				if (!ValidateStatusChange(warehouseOrder, (WarehouseOrderStatus)EditOrderStatus.SelectedItem))
                {
					EditOrderStatus.SelectedItem = warehouseOrder.WarehouseOrderStatus;
                    this.WarningWindow.Show("Można wybrać tylko sąsiadujący status.");
                    return;					
				}

                else
                {
                    var direction = ((int)warehouseOrder.WarehouseOrderStatus - (int)(WarehouseOrderStatus)EditOrderStatus.SelectedItem) == 1
                    ? StatusDirectionChange.Forward : StatusDirectionChange.Backward;
					
					WarehouseOrder.ChangeDeliveryStatus((WarehouseOrderStatus)EditOrderStatus.SelectedItem, warehouseOrder);
                    warehouseOrder.WarehouseOrderStatus = (WarehouseOrderStatus)EditOrderStatus.SelectedItem;

                    if (direction == StatusDirectionChange.Forward)
                    {
						
						if (warehouseOrder.WarehouseOrderStatus == WarehouseOrderStatus.Delivered)
                        {
							WarehouseOrder.AddProductQuantityToState(warehouseOrder);

                            OrderReceiver.Text = SWAM.MainWindow.LoggedInUser.Name;
                        }
                    }

                    if (direction == StatusDirectionChange.Backward)
                    {						
						if (warehouseOrder.WarehouseOrderStatus == WarehouseOrderStatus.InDelivery)
                        {
							WarehouseOrder.SubtractProductQuantityFromState(warehouseOrder);
                        }
                    }

					if (warehouseOrder.WarehouseOrderStatus == WarehouseOrderStatus.Delivered) CancelOrder.IsEnabled = false;
					else CancelOrder.IsEnabled = true;

					DataContext = new ApplicationDbContext();
					DataContext = warehouseOrder;
				}

            }
        }

        private void ConfirmPaymentStatusChange_Button(object sender, RoutedEventArgs e)
        {
            if (DataContext is WarehouseOrder warehouseOrder && EditPaymentStatus.SelectedItem != null)
            {
                WarehouseOrder.ChangePaymentStatus((PaymentStatus)EditPaymentStatus.SelectedItem, warehouseOrder);
                DataContext = new ApplicationDbContext();
                DataContext = warehouseOrder;
            }
        }

		private void DeleteProduct_Click(object sender, RoutedEventArgs e)
		{			
			if (((FrameworkElement)sender).DataContext is WarehouseOrderPosition warehouseOrderPosition &&
				warehouseOrderPosition.WarehouseOrder.WarehouseOrderStatus != WarehouseOrderStatus.Delivered)
			{
				this.ConfirmWindow.Show("Czy na pewno usunąć produkt?", out bool response);

				if (response)
				{
					if (warehouseOrderPosition.WarehouseOrder.WarehouseOrderStatus == WarehouseOrderStatus.Delivered)
					{
						return;
					}

					else if (WarehouseOrder.CountPositions(warehouseOrderPosition) == 1)
					{
						var WarehouseOrdersContext = new ApplicationDbContext();
						WarehouseOrdersContext.WarehouseOrders.Remove(WarehouseOrdersContext.WarehouseOrders.FirstOrDefault(o => o.Id == warehouseOrderPosition.WarehouseOrder.Id));
						WarehouseOrdersContext.SaveChanges();

						WarehouseOrderListViewModel.Instance.Refresh();
						Container.Visibility = Visibility.Hidden;
						Content.Children.Add(new CreateNewWarehouseOrderTemplate());
						DataContext = null;
					}

					else
					{
						var newContext = warehouseOrderPosition.WarehouseOrder;
						WarehouseOrder.DeleteProduct(warehouseOrderPosition);
						DataContext = new ApplicationDbContext();
						DataContext = newContext;
					}
				}
			}

			else
				return;		
		}

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is WarehouseOrder warehouseOrder && warehouseOrder.WarehouseOrderStatus != WarehouseOrderStatus.Delivered)
            {
                var context = new ApplicationDbContext();
                this.ConfirmWindow.Show("Czy na pewno usunąć zamówienie?", out bool response);

				if (response)
				{
					context.WarehouseOrders.Remove(context.WarehouseOrders.SingleOrDefault(o => o.Id == warehouseOrder.Id));
					context.SaveChanges();

                    WarehouseOrderListViewModel.Instance.Refresh();

                    if (SWAM.MainWindow.FindParent<WarehouseOrdersPanelTemplate>(this) is WarehouseOrdersPanelTemplate parent)
                        parent.SwitchBetweenOrdersAndCreating(BookmarkInPage.NewWarehouseOrder);
				}
			}
		}

        private void UpdateQuantity(object sender, RoutedEventArgs e)
        {
            var quantity = sender as TextBox;
            var warehouseOrderPosition = quantity.DataContext as WarehouseOrderPosition;

            if (quantity.Text == string.Empty)
                return;

            else if (quantity.Text == "0")
            {
                quantity.Text = "1";
                warehouseOrderPosition.Quantity = 1;
                return;
            }

            else
            {
                WarehouseOrder.updateQuantity(warehouseOrderPosition, int.Parse(quantity.Text));
            }
        }

        #region QuantityValidation
        /// <summary>
        /// Validate if characters in product quantity texbox are integers, don't allow other characters to be writen	
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuantityValidation(object sender, TextCompositionEventArgs e)
        {			
			TextBox quanity = sender as TextBox;
            Regex regex = new Regex("[^1-9]+");

            if (quanity.Text.Length > 0)
                regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }
		#endregion



		private bool ValidateStatusChange(WarehouseOrder warehouseOrder, WarehouseOrderStatus selectedItem)
        {
            if (Math.Abs(warehouseOrder.WarehouseOrderStatus - selectedItem) > 1)
                return false;
            return true; 

		}
    }
}
