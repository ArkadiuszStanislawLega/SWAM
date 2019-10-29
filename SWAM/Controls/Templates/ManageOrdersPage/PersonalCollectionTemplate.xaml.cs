﻿using SWAM.Models.ViewModels.CreateNewCustomerOrder;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for PersonalCollectionTemplate.xaml
    /// </summary>
    public partial class PersonalCollectionTemplate : UserControl
    {
        public PersonalCollectionTemplate()
        {
            InitializeComponent();
            warehouseAddress.DataContext = WarehouseInformationViewModel.Instance.Warehouse;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            warehouseAddress.DataContext = WarehouseInformationViewModel.Instance.Warehouse;
        }
    }
}