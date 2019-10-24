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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSupplierDeliveryItemListViewModelTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierDeliveryItemListViewModelTemplate : UserControl
    {
        public ExternalSupplierDeliveryItemListViewModelTemplate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<ExternalSupplierDeliverySmallItemListViewModelTemplate>(this) is ExternalSupplierDeliverySmallItemListViewModelTemplate parent)
            {
                if (parent.IsOpen)
                {
                    var story = (Storyboard)this.Profile.FindResource("HideProfileStory");
                    this.BeginStoryboard(story);
                    //BeginStoryboard((Storyboard)this.FindResource("HideProfileStory"));
                    parent.IsOpen = false;
                }
            }
        }
    }
}
