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

        #region EditAddress
        /// <summary>
        /// Change visibility of controls for edit mode.
        /// </summary>
        public void EditAddress()
        {
            this.EditCity.Visibility = Visibility.Visible;
            this.EditCountry.Visibility = Visibility.Visible;
            this.EditHouseNumber.Visibility = Visibility.Visible; 
            this.EditPostCode.Visibility = Visibility.Visible; 
            this.EditStreet.Visibility = Visibility.Visible;
            this.EditApartmentNumber.Visibility = Visibility.Visible;

            this.City.Visibility = Visibility.Collapsed;
            this.Country.Visibility = Visibility.Collapsed;
            this.HouseNumber.Visibility = Visibility.Collapsed; 
            this.PostCode.Visibility = Visibility.Collapsed; 
            this.Street.Visibility = Visibility.Collapsed; 
            this.ApartmentNumber.Visibility = Visibility.Collapsed;
        }
        #endregion

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
    }
}
