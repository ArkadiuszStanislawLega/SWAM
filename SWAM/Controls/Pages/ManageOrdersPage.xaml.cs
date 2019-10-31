using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SWAM.Controls.Templates.MainWindow;
using SWAM.Controls.Templates.ManageOrdersPage.Customers;
using SWAM.Controls.Templates.ManageOrdersPage.Warehouses;
using SWAM.Enumerators;
using SWAM.Models.Customer;

namespace SWAM.Controls.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy ManageOrdersPage.xaml
    /// </summary>
    public partial class ManageOrdersPage : BasicPage
    {
        private readonly Dictionary<BookmarkInPage, UserControl> _userControls = new Dictionary<BookmarkInPage, UserControl>(){
                { BookmarkInPage.WarehouseOrdersPanel, new WarehouseOrdersPanelTemplate()},
                { BookmarkInPage.CustomerOrdersPanel, new CreateNewCustomerOrderTemplate() }
        };
        /// <summary>
        /// Current visible bookmark.
        /// </summary>
        UserControl _currentContent;

        public ManageOrdersPage()
        {
            InitializeComponent();
        }

        private void SwitchToWarehouseOrdersControlPanel_Click(object sender, System.Windows.RoutedEventArgs e) => ChangeThisContent(BookmarkInPage.WarehouseOrdersPanel);
        private void SwitchToCustomerOrdersControlPanel_Click(object sender, System.Windows.RoutedEventArgs e) => ChangeThisContent(BookmarkInPage.CustomerOrdersPanel);

        #region ChangeThisContent
        /// <summary>
        /// Setting properties buttons depending on whether it is pressed.
        /// And changing main content of page.
        /// </summary>
        /// <param name="bookmarkManageOrders">New bookmark.</param>
        private void ChangeThisContent(BookmarkInPage bookmarkManageOrders)
        {
            //Find selected button
            foreach (NavigationButtonTemplate nvb in this.NavigationBar.Children)
            {
                if (nvb.Bookmark == bookmarkManageOrders) nvb.IsSelected = true;
                else nvb.IsSelected = false;
            }

            this._userControls.TryGetValue(bookmarkManageOrders, out UserControl userControl);
            ChangeContext(userControl);
        }
        #endregion+

        #region ChangeContext
        /// <summary>
        /// Changing main content of ManageOrdersPage.
        /// </summary>
        /// <param name="manageOrdersConmtrolPanel">New content.</param>
        private void ChangeContext(UserControl manageOrdersConmtrolPanel)
        {
            if (manageOrdersConmtrolPanel != this._currentContent)
            {
                this._currentContent = manageOrdersConmtrolPanel;
                if (this.MainContent.Children.Count > 0)
                    this.MainContent.Children.RemoveAt(this.MainContent.Children.Capacity - 1);

                this.MainContent.Children.Add(this._currentContent);
            }
        }
        #endregion

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
