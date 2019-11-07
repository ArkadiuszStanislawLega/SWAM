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

            var newAddress = this.Address.GetAddress<WarehouseAddress>();//Take address from view 
            if (newAddress != null)
            {
                if (this.WarehouseName.Text != string.Empty)
                {
                    if (!Warehouse.IsWarehouseNameExist(this.WarehouseName.Text))
                    {
                        if (long.TryParse(this.pHeight.Text, out long isHeight) && isHeight > 0)
                        {
                            if (long.TryParse(this.pWidth.Text, out long isWidth) && isWidth > 0)
                            {
                                if (long.TryParse(this.pLength.Text, out long isLength) && isLength > 0)
                                {
                                    if (long.TryParse(this.pAcceptableWeight.Text, out long isAcceptableWeight) && isAcceptableWeight > 0)
                                    {
                                        if (Warehouse.IsAdd(new Warehouse()
                                        {
                                            Name = this.WarehouseName.Text,
                                            WarehouseAddress = newAddress,
                                            Height = isHeight,
                                            Width = isWidth,
                                            Length = isLength,
                                            AcceptableWeight = isAcceptableWeight
                                        }))
                                        {
                                            InformationToUser($"Został utworzony nowy magazn - {this.WarehouseName.Text}.");
                                            isWarehouseCreated = true;
                                        }
                                    }
                                    else BadValueMessage(this.tbAcceptableWeight.Text);
                                }
                                else BadValueMessage(this.tbLength.Text);
                            }
                            else BadValueMessage(this.tbWidth.Text);
                        }
                        else BadValueMessage(this.tbHeight.Text);
                    }
                    else InformationToUser($"Nazwa {this.WarehouseName.Text} jest już używana.", true);
                }
                else BadValueMessage(this.tbWarehouseName.Text);

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
            this.pAcceptableWeight.Text = "";
        }
        #endregion  
    }
}
