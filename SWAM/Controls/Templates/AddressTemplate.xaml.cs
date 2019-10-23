using System.Windows.Controls;
using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System.Windows.Media.Animation;
using System;
using SWAM.Models.ExternalSupplier;

namespace SWAM.Controls.Templates
{
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy Address.xaml
    /// </summary>
    public partial class AddressTemplate : UserControl
    {
        public AddressTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starting storyboard after which the data is in the edit mode.
        /// </summary>
        public void ShowEditControls() => BeginStoryboard(FindResource("ShowEditStory") as Storyboard);
        /// <summary>
        /// Starting storyboard after which the data is in the read only mode.
        /// </summary>
        public void HideEditControls() => BeginStoryboard(FindResource("HideEditStory") as Storyboard);
        #region ClearEditValues
        /// <summary>
        /// Make all TextBoxes field text clear.
        /// </summary>
        public void ClearEditValues()
        {
            this.EditApartmentNumber.Text = string.Empty;
            this.EditCity.Text = string.Empty;
            this.EditCountry.Text = string.Empty;
            this.EditHouseNumber.Text = string.Empty;
            this.EditPostCode.Text = string.Empty;
            this.EditStreet.Text = string.Empty;
        }
        #endregion

        public Address GetAddress()
        {
            if (this.EditCountry.Text != string.Empty && 
                this.EditPostCode.Text != string.Empty && 
                this.EditCity.Text != string.Empty && 
                this.EditStreet.Text != string.Empty && 
                this.EditHouseNumber.Text != string.Empty)
            {
                return new Address()
                {
                    Country = this.EditCountry.Text,
                    PostCode = this.EditPostCode.Text,
                    City = this.EditCity.Text,
                    Street = this.EditStreet.Text,
                    HouseNumber = this.EditHouseNumber.Text,
                    ApartmentNumber = this.EditApartmentNumber.Text,
                };
            }
            else return null;
        }

        #region GetAddress - generic
        /// <summary>
        /// Retrieves the entered data and returns as the type of class that was entered as generic.
        /// </summary>
        /// <typeparam name="T">The type of class to be returned.</typeparam>
        /// <returns>Address which has been entered into the form.</returns>
        public T GetAddress<T>() where T: class, new()
        {
            if (this.EditCountry.Text != string.Empty && 
                this.EditPostCode.Text != string.Empty && 
                this.EditCity.Text != string.Empty && 
                this.EditStreet.Text != string.Empty && 
                this.EditHouseNumber.Text != string.Empty)
            {
                if (typeof(T) == typeof(WarehouseAddress))
                {
                    var warehouseAddress = new WarehouseAddress()
                    {
                        Country = this.EditCountry.Text,
                        PostCode = this.EditPostCode.Text,
                        City = this.EditCity.Text,
                        Street = this.EditStreet.Text,
                        HouseNumber = this.EditHouseNumber.Text,
                        ApartmentNumber = this.EditApartmentNumber.Text,
                    };

                    return (T)Convert.ChangeType(warehouseAddress, typeof(T));
                }
                else if (typeof(T) == typeof(CustomerOrderDeliveryAddress))
                {
                    var customerDeliveryAddress = new CustomerOrderDeliveryAddress()
                    {
                        Country = this.EditCountry.Text,
                        PostCode = this.EditPostCode.Text,
                        City = this.EditCity.Text,
                        Street = this.EditStreet.Text,
                        HouseNumber = this.EditHouseNumber.Text,
                        ApartmentNumber = this.EditApartmentNumber.Text,
                    };

                    return (T)Convert.ChangeType(customerDeliveryAddress, typeof(T));
                }
                else if (typeof(T) == typeof(CustomerResidentalAddress))
                {
                    var cusomterResidentalAddress = new CustomerResidentalAddress()
                    {
                        Country = this.EditCountry.Text,
                        PostCode = this.EditPostCode.Text,
                        City = this.EditCity.Text,
                        Street = this.EditStreet.Text,
                        HouseNumber = this.EditHouseNumber.Text,
                        ApartmentNumber = this.EditApartmentNumber.Text,
                    };

                    return (T)Convert.ChangeType(cusomterResidentalAddress, typeof(T));
                }
                else if(typeof(T) == typeof(ExternalSupplierAddress))
                {
                    var externalSupplierAddress = new ExternalSupplierAddress()
                    {
                        Country = this.EditCountry.Text,
                        PostCode = this.EditPostCode.Text,
                        City = this.EditCity.Text,
                        Street = this.EditStreet.Text,
                        HouseNumber = this.EditHouseNumber.Text,
                        ApartmentNumber = this.EditApartmentNumber.Text,
                    };

                    return (T)Convert.ChangeType(externalSupplierAddress, typeof(T));
                }
            }

            return null;
        }
        #endregion
    }
}
