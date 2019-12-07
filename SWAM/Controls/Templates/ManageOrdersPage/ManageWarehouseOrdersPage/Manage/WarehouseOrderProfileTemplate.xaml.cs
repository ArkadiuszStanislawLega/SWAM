using SWAM.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWAM.Models.Warehouse;
using SWAM.Models.ExternalSupplier;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using SWAM.Strings;
using SWAM.Models.User;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.Manage
{
	/// <summary>
	/// Logika interakcji dla klasy WarehouseOrderProfileTemplate.xaml
	/// </summary>
	/// 


	
public partial class WarehouseOrderProfileTemplate : UserControl
	{
		public WarehouseOrderProfileTemplate()
		{
			InitializeComponent();

		}
		
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{			
			if (DataContext is WarehouseOrder warehouseOrder)
			{				
				WarehouseName.Text = warehouseOrder.Warehouse.Name;
				CreatorName.Text = warehouseOrder.Creator.Name;
				SupplierName.Text = warehouseOrder.ExternalSupplayer.Name;
				if (warehouseOrder.UserReceivedOrder != null) OrderReceiver.Text = warehouseOrder.UserReceivedOrder.Name;
			}
				

		}
		
		private void ConfirmStatusChange_Button(object sender, RoutedEventArgs e)
		{
			if (DataContext is WarehouseOrder warehouseOrder && EditOrderStatus.SelectedItem != null)
			{
				WarehouseOrder.ChangeDeliveryStatus((WarehouseOrderStatus) EditOrderStatus.SelectedItem, warehouseOrder);
				DataContext = new ApplicationDbContext();
				DataContext = warehouseOrder;
				if (warehouseOrder.WarehouseOrderStatus == WarehouseOrderStatus.Delivered) OrderReceiver.Text = SWAM.MainWindow.LoggedInUser.Name;
			}
		} 

		private void ConfirmPaymentStatusChange_Button(object sender, RoutedEventArgs e)
		{
			if (DataContext is WarehouseOrder warehouseOrder && EditPaymentStatus.SelectedItem != null)
			{
				WarehouseOrder.ChangePaymentStatus((PaymentStatus) EditPaymentStatus.SelectedItem, warehouseOrder);
				DataContext = new ApplicationDbContext();
				DataContext = warehouseOrder;
			}
		}



	}
}
