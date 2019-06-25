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

            this.Address.EditAddress();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newAddress = this.Address.GetAddress();

                context.Addresses.Add(newAddress);
                context.SaveChanges();

                var dbAddress = context.Addresses.FirstOrDefault(a =>
                a.Country == newAddress.Country &&
                a.PostCode == newAddress.PostCode &&
                a.City == newAddress.City &&
                a.Street == newAddress.Street &&
                a.HouseNumber == newAddress.HouseNumber &&
                a.ApartmentNumber == newAddress.ApartmentNumber);

                context.Warehouses.Add(new Models.Warehouse()
                {
                    Name = Name.Text,
                    AddressId = dbAddress.Id
                });
                context.SaveChanges();
            }
        }

        private void Address_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
