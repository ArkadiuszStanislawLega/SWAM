using SWAM.Controls.Templates.CustomerPage;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.CouriersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CourierORderItemListViewTemplate.xaml
    /// </summary>
    public partial class CourierORderItemListViewTemplate : UserControl
    {
        public CourierORderItemListViewTemplate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SWAM.MainWindow.FindParent<CourierOrderItemSmallListViewTemplate>(this) is CourierOrderItemSmallListViewTemplate parent)
            {
                if (parent.IsOpen)
                {
                    var story = (Storyboard)this.FindResource("HideProfileStory");
                    this.BeginStoryboard(story);
                    parent.IsOpen = false;
                }
            }
        }
    }
}
