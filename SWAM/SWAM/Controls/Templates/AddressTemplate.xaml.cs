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
            TurnOffValues();
            TurnOnEditControls();
        }
        #endregion
        #region TurnOnEditControls
        /// <summary>
        /// Turning on all edit controls.
        /// </summary>
        private void TurnOnEditControls()
        {
            TurnOn(this.EditCountry);
            TurnOn(this.EditPostCode);
            TurnOn(this.EditCity);
            TurnOn(this.EditStreet);
            TurnOn(this.EditHouseNumber);
            TurnOn(this.EditApartmentNumber);
        }
        #endregion
        #region TurnOffValues
        /// <summary>
        /// Turning off visibility of user profile values.
        /// </summary>
        private void TurnOffValues()
        {
            TurnOff(this.Country);
            TurnOff(this.PostCode);
            TurnOff(this.City);
            TurnOff(this.Street);
            TurnOff(this.HouseNumber);
            TurnOff(this.ApartmentNumber);
        }
        #endregion

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
