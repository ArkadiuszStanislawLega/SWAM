using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSupplierDeliverySmallItemListViewModelTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierDeliverySmallItemListViewModelTemplate : UserControl
    {
        /// <summary>
        /// Flag indicating whether the order profile is expanded.
        /// </summary>
        public bool IsOpen = false;
        public ExternalSupplierDeliverySmallItemListViewModelTemplate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.IsOpen)
            {
                this.BeginStoryboard((Storyboard)this.FindResource("ShowProfileStory"));
                this.IsOpen = true;
            }
        }
    }
}
