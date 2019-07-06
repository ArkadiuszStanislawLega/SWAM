using SWAM.Templates.AdministratorPage;
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

namespace SWAM.Controls.Templates.AdministratorPage.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewWarehouseTemplate.xaml
    /// </summary>
    public partial class CreateNewWarehouseTemplate : BasicUserControl
    {
        public CreateNewWarehouseTemplate()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newAddress = this.Address.GetAddress();//Take address from view 

                context.Addresses.Add(newAddress);
                context.SaveChanges();

                var dbAddress = context.Addresses.FirstOrDefault(a =>
                a.Country == newAddress.Country &&
                a.PostCode == newAddress.PostCode &&
                a.City == newAddress.City &&
                a.Street == newAddress.Street &&
                a.HouseNumber == newAddress.HouseNumber &&
                a.ApartmentNumber == newAddress.ApartmentNumber); //Take address with database address Id

                context.Warehouses.Add(new Models.Warehouse()
                {
                    Name = Name.Text,
                    AddressId = dbAddress.Id
                });
                context.SaveChanges(); //Add warehouse to database
            }

            //Refresh List with warehouses
            if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                template.RefreshList();

            //Restart all controls
            this.Name.Text = "";
            this.Address.ClearEditValues();
        }

        private void Address_Loaded(object sender, RoutedEventArgs e) => this.Address.ShowEditControls();
        
    }
}
