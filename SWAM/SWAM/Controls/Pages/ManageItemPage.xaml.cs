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
using SWAM.Controls.Windows;
using SWAM.Enumerators;

namespace SWAM
{
    /// <summary>
    /// Logika interakcji dla klasy ManageItemPage.xaml
    /// </summary>
    public partial class ManageItemPage : UserControl
    {
        #region BasicConstructor
        public ManageItemPage() 
        {
            InitializeComponent();
            DataContext = this;
            
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

        private List<Item> items = new List<Item>
        {
            new Item(1, "Cegła", 10, 2, 0.2, 2.5),            
            new Item(2, "Taczka", 60, 100, 6, 60),
            new Item(3, "Piasek", 15, 80, 15, 10),
            new Item(3, "Piasek", 15, 80, 15, 10),
            new Item(4, "Deska", 20, 60, 0.5, 5),
            new Item(5, "Kostka brukowa", 10, 10, 0.33, 2),
            new Item(6, "Taczka", 70, 110, 6.5, 70),
            new Item(7, "Wiadro", 30, 40, 1.5, 15),
        };

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<AddProductWindow>().Any()==true)
            {
                MessageBox.Show("Okno jest już otworzone.", "Okno otworzone", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                AddProductWindow _addWindow = new AddProductWindow();
                _addWindow.Show();
            }
         }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<EditProductWindow>().Any() == true)
            {
                MessageBox.Show("Okno jest już otworzone.", "Okno otworzone", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                EditProductWindow _editWindow = new EditProductWindow();
                _editWindow.Show();
            }                      
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<DeleteProductWindow>().Any() == true)
            {
                MessageBox.Show("Okno jest już otworzone.", "Okno otworzone", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                DeleteProductWindow _deleteWindow = new DeleteProductWindow();
                _deleteWindow.Show();
            }           
        }
       
    }
}
