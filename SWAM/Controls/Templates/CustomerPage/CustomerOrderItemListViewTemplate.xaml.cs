using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrderItemListViewTemplate.xaml
    /// </summary>
    public partial class CustomerOrderItemListViewTemplate : UserControl
    {
        List<CustomerOrderPosition> orderPositions = new List<CustomerOrderPosition>();
        public CustomerOrderItemListViewTemplate()
        {
            InitializeComponent();
        }
    }
}
