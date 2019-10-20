using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.CouriersPage
{
    /// <summary>
    /// Logika interakcji dla klasy CourierOrderItemSmallListViewTemplate.xaml
    /// </summary>
    public partial class CourierOrderItemSmallListViewTemplate : UserControl
    {
        /// <summary>
        /// Flag indicating whether the order profile is expanded.
        /// </summary>
        public bool IsOpen = false;

        public CourierOrderItemSmallListViewTemplate()
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
