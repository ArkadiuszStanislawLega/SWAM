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


                if (long.TryParse(pHeight.Text, out long isHeight))
                {
                    if (long.TryParse(pWidth.Text, out long isWidth))
                    {
                        if (long.TryParse(pSurfaceAreaNetto.Text, out long isSurfaceAreaNetto))
                        {
                            if (long.TryParse(pSurfaceAreaBrutton.Text, out long isSurfaceAreaBrutton))
                            {
                                if (long.TryParse(pAcceptableWeight.Text, out long isAcceptableWeight))
                                {
                                    context.Warehouses.Add(new Models.Warehouse()
                                    {
                                        Name = WarehouseName.Text,
                                        AddressId = dbAddress.Id,
                                        Height = isHeight,
                                        Width = isWidth,
                                        SurfaceAreaNetto = isSurfaceAreaNetto,
                                        SurfaceAreaBrutton = isSurfaceAreaBrutton,
                                        AcceptableWeight = isAcceptableWeight
                                    });
                                    context.SaveChanges(); //Add warehouse to database
                                }
                                else InformationToUser("Sprawdź wartość dopuszczalna waga - jest nie poprawna.", true);
                            }
                            else InformationToUser("Sprawdź wartość powierzchnia brutton - jest nie poprawna.", true);
                        }
                        else InformationToUser("Sprawdź wartość powierzchnia netto - jest nie poprawna.", true);
                    }
                    else InformationToUser("Sprawdź wartość szerokość - jest nie poprawna.", true);
                }
                else InformationToUser("Sprawdź wartość wysokość - jest nie poprawna.", true);
            }

            //Refresh List with warehouses
            if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                template.RefreshList();

            //Restart all controls
            this.WarehouseName.Text = "";
            this.Address.ClearEditValues();
        }

        private void Address_Loaded(object sender, RoutedEventArgs e) => this.Address.ShowEditControls();
        
    }
}
