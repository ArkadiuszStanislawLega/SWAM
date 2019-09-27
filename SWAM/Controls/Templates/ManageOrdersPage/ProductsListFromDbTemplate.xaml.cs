using SWAM.Models;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage
{
    /// <summary>
    /// Interaction logic for ProductsListFromDbTemplate.xaml
    /// </summary>
    public partial class ProductsListFromDbTemplate : UserControl
    {
        public ProductsListFromDbTemplate()
        {
            InitializeComponent();
        }

        private void NumberRowIteration(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
