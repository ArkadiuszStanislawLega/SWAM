﻿using System;
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

namespace SWAM.Controls.Templates
{
    using SWAM.Models.Customer;
    using SWAM.Models.Warehouse;
    using System.Windows.Media.Animation;
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


        public void ClearEditValues()
        {
            this.EditApartmentNumber.Text = "";
            this.EditCity.Text = "";
            this.EditCountry.Text = "";
            this.EditHouseNumber.Text = "";
            this.EditPostCode.Text = "";
            this.EditStreet.Text = "";
        }

        public Address GetAddress()
        {
            if (this.EditCountry.Text != "" && this.EditPostCode.Text != "" && this.EditCity.Text != "" && this.EditStreet.Text != "" && this.EditHouseNumber.Text != "")
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

        public CusomterResidentalAddress GetCustomerResidenAddress()
        {
            if (this.EditCountry.Text != "" && this.EditPostCode.Text != "" && this.EditCity.Text != "" && this.EditStreet.Text != "" && this.EditHouseNumber.Text != "")
            {
                return new CusomterResidentalAddress()
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

        public CustomerDeliveryAddress GetCustomerDeliveryAddress()
        {
            if (this.EditCountry.Text != "" && this.EditPostCode.Text != "" && this.EditCity.Text != "" && this.EditStreet.Text != "" && this.EditHouseNumber.Text != "")
            {
                return new CustomerDeliveryAddress()
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


        public WarehouseAddress GetWarehouseAddress()
        {
            if (this.EditCountry.Text != "" && this.EditPostCode.Text != "" && this.EditCity.Text != "" && this.EditStreet.Text != "" && this.EditHouseNumber.Text != "")
            {
                return new WarehouseAddress()
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


    }
}
