using SWAM.Enumerators;
using SWAM.Models.Customer;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ManageOrdersPage.Customers
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerOrdersPanelTemplate.xaml
    /// </summary>
    public partial class CustomerOrdersPanelTemplate : UserControl
    {
        public BookmarkInPage CurrentBookmarkLoaded { get; private set; }

        public CustomerOrdersPanelTemplate()
        {
            InitializeComponent();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SwitchBetweenOrdersAndCreating(UserControl newContent)
        {
            if (this.MainContent.Children.Count > 0)
                this.MainContent.Children.RemoveAt(this.MainContent.Children.Count - 1);

            this.MainContent.Children.Add(newContent);
        }

        private void AddNewCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CreateNewCustomerOrder;
            SwitchBetweenOrdersAndCreating(new CreateNewCustomerOrderTemplate());
        }


        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Item_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CurrentBookmarkLoaded = BookmarkInPage.CustomerOrderProfile;
            if (sender is Button button && button.DataContext is CustomerOrder customerOrder)
            {
                SwitchBetweenOrdersAndCreating(new CustomerOrderProfileTemplate() { DataContext = customerOrder });
            }
        }

    }
}
