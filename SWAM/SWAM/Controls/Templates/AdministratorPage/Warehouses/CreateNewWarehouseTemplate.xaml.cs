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

        private void Address_Loaded(object sender, RoutedEventArgs e) => this.Address.ShowEditControls();

        #region Confirm_Click
        /// <summary>
        /// Action after click create new warehouse button.
        /// </summary>
        /// <param name="sender">Create new warehouse button</param>
        /// <param name="e">Event button is clicked</param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool isWarehouseCreated = false; //Flag indicating whether a new warehouse was created

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

                if (WarehouseName.Text != "")
                {
                    if (long.TryParse(pHeight.Text, out long isHeight) && isHeight > 0)
                    {
                        if (long.TryParse(pWidth.Text, out long isWidth) && isWidth > 0)
                        {
                            if (long.TryParse(pSurfaceAreaNetto.Text, out long isSurfaceAreaNetto) && isSurfaceAreaNetto > 0)
                            {
                                if (long.TryParse(pSurfaceAreaBrutton.Text, out long isSurfaceAreaBrutton) && isSurfaceAreaBrutton > 0)
                                {
                                    if (long.TryParse(pAcceptableWeight.Text, out long isAcceptableWeight) && isAcceptableWeight > 0)
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
                                        InformationToUser($"Został utworzony nowy magazn - {WarehouseName.Text}.");
                                        isWarehouseCreated = true;
                                    }
                                    else BadValueMessage("dopuszczalna waga");
                                }
                                else BadValueMessage("powierzchnia brutton");
                            }
                            else BadValueMessage("powierzchnia netto");
                        }
                        else BadValueMessage("szerokość"); 
                    }
                    else BadValueMessage("wysokość"); 
                }
                else BadValueMessage("nazwa magazynu");//TODO: Zastanowić się nad validacją tej samej nazwy magazynu.
            }

            //Refresh List with warehouses
            if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                template.RefreshList();
            else InformationToUser($"{ErrorMesages.DURGIN_ADD_WAREHOUSE_ERROR} {ErrorMesages.REFRESH_WAREHOUSES_LIST_ERROR}", true);

            //Restart all controls
            if (isWarehouseCreated)
            {
                this.WarehouseName.Text = "";
                this.Address.ClearEditValues();
            }
        }
        #endregion

        /// <summary>
        /// Information to user with red background about typed wrong value. 
        /// </summary>
        /// <param name="value">Property which is incorrect</param>
        private void BadValueMessage(string value) => InformationToUser($"Sprawdź wartość - {value} - jest niepoprawna.", true);
    }
}
