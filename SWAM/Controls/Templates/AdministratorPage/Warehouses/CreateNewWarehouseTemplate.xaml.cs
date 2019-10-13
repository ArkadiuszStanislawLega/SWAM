using SWAM.Templates.AdministratorPage;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using SWAM.Strings;
using System;
using SWAM.Models.Warehouse;

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

            //TODO: Try - catch
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newAddress = this.Address.GetAddress<WarehouseAddress>();//Take address from view 
                if (newAddress != null)
                {
                    context.Adresses.Add(newAddress);
                    context.SaveChanges();

                    var dbAddress = context.WarehouseAddresses.FirstOrDefault(a =>
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
                                if (long.TryParse(pLength.Text, out long isLength) && isLength > 0)
                                {
                                    if (long.TryParse(pSurfaceAreaNetto.Text, out long isSurfaceAreaNetto) && isSurfaceAreaNetto > 0)
                                    {
                                        if (long.TryParse(pSurfaceAreaBrutton.Text, out long isSurfaceAreaBrutton) && isSurfaceAreaBrutton > 0)
                                        {
                                            if (long.TryParse(pAcceptableWeight.Text, out long isAcceptableWeight) && isAcceptableWeight > 0)
                                            {
                                                context.Warehouses.Add(new Warehouse()
                                                {
                                                    Name = WarehouseName.Text,
                                                    WarehouseAddress = dbAddress,
                                                    Height = isHeight,
                                                    Width = isWidth,
                                                    Length = isLength,
                                                    SurfaceAreaNetto = isSurfaceAreaNetto,
                                                    SurfaceAreaBrutton = isSurfaceAreaBrutton,
                                                    AcceptableWeight = isAcceptableWeight
                                                });
                                                context.SaveChanges(); //Add warehouse to databas
                                                InformationToUser($"Został utworzony nowy magazn - {WarehouseName.Text}.");
                                                isWarehouseCreated = true;
                                            }
                                            else BadValueMessage(tbAcceptableWeight.Text);
                                        }
                                        else BadValueMessage(tbSurfaceAreaBrutton.Text);
                                    }
                                    else BadValueMessage(tbSurfaceAreaNetto.Text);
                                }
                                else BadValueMessage(tbLength.Text);
                            }
                            else BadValueMessage(tbWidth.Text);
                        }
                        else BadValueMessage(tbHeight.Text);
                    }
                    else BadValueMessage(tbWarehouseName.Text);//TODO: Consider validating the same warehouse name.

                    //Refresh List with warehouses
                    if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                        template.RefreshList();
                    else InformationToUser($"{ErrorMesages.DURGIN_ADD_WAREHOUSE_ERROR} {ErrorMesages.REFRESH_WAREHOUSES_LIST_ERROR}", true);

                    //Restart all controls
                    if (isWarehouseCreated)
                    {
                        this.WarehouseName.Text = "";
                        this.Address.ClearEditValues();
                        this.ClearTechnicalDataControls();
                    }
                }
                else BadValueMessage("Adres");
            }
        }
        #endregion

        /// <summary>
        /// Information to user with red background about typed wrong value. 
        /// </summary>
        /// <param name="incorrectValue">Property which is incorrect</param>
        private void BadValueMessage(string incorrectValue) => InformationToUser($"Sprawdź wartość - {incorrectValue} - jest niepoprawna.", true);

        #region TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] charArray;

            Regex regex = new Regex("[^0-9]+");
            if (sender is TextBox textBox && regex.IsMatch(textBox.Text))
            {
                var values = textBox.Text.ToList();
                values.RemoveAt(values.Count - 1);

                charArray = values.ToArray();

                textBox.Text = new string(charArray);
                InformationToUser($"Podając tą wartość możesz użyć tylko cyfr.", true);
            }
            else InformationToUser("");
        }
        #endregion
        #region ClearControls
        /// <summary>
        /// Clear all TextBlocks.
        /// </summary>
        public void ClearTechnicalDataControls()
        {
            this.pHeight.Text = "";
            this.pWidth.Text = "";
            this.pLength.Text = "";
            this.pSurfaceAreaNetto.Text = "";
            this.pSurfaceAreaBrutton.Text = "";
            this.pAcceptableWeight.Text = "";
        }
        #endregion  
    }
}
