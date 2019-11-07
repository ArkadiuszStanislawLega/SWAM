using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Courier;
using System.Windows;

namespace SWAM.Controls.Templates.CouriersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewCourierTemplate.xaml
    /// </summary>
    public partial class CreateNewCourierTemplate : BasicUserControl
    {
        public CreateNewCourierTemplate()
        {
            InitializeComponent();
        }

        #region AddNewCourier_Click
        /// <summary>
        /// Action after click add new courier button.
        /// Validates values and try add new courier to database.
        /// </summary>
        /// <param name="sender">Add new courire button.</param>
        /// <param name="e">Event clicked.</param>
        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {
            //Check name - The name cannot be empty
            if (this.CourierName.Text != string.Empty && !Courier.IsNameExist(this.CourierName.Text))
            {
                char[] nameLength = this.CourierName.Text.ToCharArray();
                //the name must contain more than 3 letters
                if (nameLength.Length > SWAM.MainWindow.MIN_NAME_LENGTH)
                {
                    //Validate email address.
                    if (EmailAddress.IsValidEmail(this.CourierEmailAddress.Text))
                    {
                        //TODO: Update TIN validation.
                        if (this.CourierTIN.Text != string.Empty)
                        {
                            Courier courier = new Courier()
                            {
                                Name = this.CourierName.Text,
                                Phone = this.CourierPhone.Text,
                                Tin = this.CourierTIN.Text,
                                EmailAddress = this.CourierEmailAddress.Text
                            };

                            //Try to add courier
                            if (courier.IsAdd(courier))
                            {
                                InformationToUser($"Dodano nowego kuriera {this.CourierName.Text}.");

                                this.CourierName.Text = string.Empty;
                                this.CourierPhone.Text = string.Empty;
                                this.CourierTIN.Text = string.Empty;
                                this.CourierEmailAddress.Text = string.Empty;

                                CouriersListViewModel.Instance.Refresh();
                            }
                            else InformationToUser($"Kurier o nazwie {this.CourierName.Text} już isnieje.", true);
                        }
                        else InformationToUser($"TIN jest błędny.", true);
                    }
                    else InformationToUser($"Adres e-mail {this.CourierEmailAddress.Text} jest nieprawidłowy.");
                }
                else InformationToUser($"Nazwa musi mieć więcej niż 3 litery.", true);
            }
            else InformationToUser($"Błędna nazwa.", true);
        }
        #endregion
    }
}
