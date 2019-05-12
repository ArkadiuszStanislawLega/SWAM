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

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : Page
    {
        #region Properties
        /// <summary>
        /// Name of the current page.
        /// </summary>
        new const PagesUserControls NAME_OF_PAGE = PagesUserControls.manageItemsPage;
        #endregion

        #region BasicConstructor
        public ManageItemPage(MainWindow mainWindow) 
            :base(mainWindow)
        {
            Create_sample_products();
            DataContext = this;
            InitializeComponent();
        }
        #endregion
        public class Item
        {
            public int Id_product { get; set; }
            public string Name { get; set; }
            public double Width { get; set; }
            public double Length { get; set; }
            public double Weight { get; set; }
            public double Selling_price { get; set; }


            public Item(int id_product, string name, double width, double length, double weight, double selling_price)
            {
                Id_product = id_product;
                Name = name;
                Width = width;
                Length = length;
                Weight = weight;
                Selling_price = selling_price;
            }

        }

        private void Create_sample_products()
        {
            Items.Add(new Item(1, "Cegła", 10, 2, 0.2, 2.5));
            Items.Add(new Item(2, "Taczka", 60, 100, 6, 60));
            Items.Add(new Item(3, "Piasek", 15, 80, 15, 10));
            Items.Add(new Item(4, "Deska", 20, 60, 0.5, 5));
            Items.Add(new Item(5, "Kostka brukowa", 10, 10, 0.33, 2));
            //Trace.WriteLine(Items[4].Name);

        } // tworzenie przykładowych produktów do gridu

        private List<Item> items = new List<Item>();

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }
        override protected void ChangePage_Click(object sender, RoutedEventArgs e) => this._mainWindow.ChangeContent(PagesUserControls.administratorPage);
    }
}
