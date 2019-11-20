using System.Windows.Controls;
using System.Windows.Media.Animation;


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
                    parent.IsOpen = false;
                }
            }
        }
    }
}
