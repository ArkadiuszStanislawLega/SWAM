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

        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {
            if (EmailAddress.IsValidEmail(this.CourierEmailAddress.Text))
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Couriers.Add(new Models.Courier.Courier()
                    {
                        Name = this.CourierName.Text,
                        Phone = this.CourierPhone.Text,
                        EmailAddress = this.CourierEmailAddress.Text
                    });
                    context.SaveChanges();

                    InformationToUser($"Dodano nowego kuriera {this.CourierName.Text}.");

                    CouriersListViewModel.Instance.Refresh();
                }
            }
            else InformationToUser($"Adres e-mail {this.CourierEmailAddress.Text} jest nieprawidłowy.");
        }
    }
}
