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
