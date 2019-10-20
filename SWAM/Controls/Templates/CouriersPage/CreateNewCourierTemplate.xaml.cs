using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using System.Windows;
using System.Windows.Controls;

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
                        Email = this.CourierEmailAddress.Text
                    });
                    context.SaveChanges();
                }
            }
            else InformationToUser($"Adres e-mail {this.CourierEmailAddress.Text} jest nieprawidłowy.");
        }
    }
}
