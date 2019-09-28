using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace SWAM.Models.ProductPage
{
    public class ProductListViewModel : UserControl
    {
        private readonly ObservableCollection<Product> products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products => this.products;
        public ProductListViewModel()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (products.Count > 0)
                products.Clear();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach (var item in context.Products.ToList())
                {
                    products.Add(item);
                }
            }
        }
    }
}
