using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.CouriersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewCourierTemplate.xaml
    /// </summary>
    public partial class CreateNewCourierTemplate : UserControl
    {
        public CreateNewCourierTemplate()
        {
            InitializeComponent();
        }

        private void AddNewCourier_Click(object sender, RoutedEventArgs e)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Couriers.Add(new Models.Courier.Courier()
                {
                    Name = this.CourierName.Text,
                    Phone = this.CourierPhone.Text
                });
                context.SaveChanges();
            }
        }
    }
}
