using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrderItemListViewTemplate.xaml
    /// </summary>
    public partial class CustomerOrderItemListViewTemplate : UserControl
    {
        public CustomerOrderItemListViewTemplate()
        {
            InitializeComponent();
            //this.Profile.BeginStoryboard((Storyboard)FindResource("HideProfileStory"));
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(SWAM.MainWindow.FindParent<CustomerOrderItemSmallListViewTemplate>(this) is CustomerOrderItemSmallListViewTemplate parent)
            {
                if(parent.IsOpen)
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
