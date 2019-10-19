using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrderItemSmallListViewTemplate.xaml
    /// </summary>
    public partial class CustomerOrderItemSmallListViewTemplate : UserControl
    {
        /// <summary>
        /// Flag indicating whether the order profile is expanded.
        /// </summary>
        public bool IsOpen = false;

        public CustomerOrderItemSmallListViewTemplate()
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
