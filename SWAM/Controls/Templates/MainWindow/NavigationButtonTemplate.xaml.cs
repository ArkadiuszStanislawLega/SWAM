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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWAM.Enumerators;
using SWAM.Events.NavigationButton;

namespace SWAM.Controls.Templates.MainWindow
{
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy NavigationButtonTemplate.xaml
    /// The button pressed once will remain pressed until the IsSelected flag is marked.
    /// To use this button correctly you should create in BookmarkInPage enum with your tab or PagesUserControls with your page.
    /// Then assign a successive group of buttons depending on which side you want to open.
    /// </summary>
    public partial class NavigationButtonTemplate : Button
    {
        #region Event&Handler
        public delegate void SelectedHandler(NavigationButtonTemplate button, ButtonSelectedArgs isSelectedArg);
        public event SelectedHandler IsSelectedEvent;
        #endregion
        #region Properties
        /// <summary>
        /// Flag indicating whether the button is pressed.
        /// </summary>
        private bool _isSeleceted;

        /// <summary>
        /// Shows the page for which the button corresponds.
        /// </summary>
        private PagesUserControls _pageToOpen = PagesUserControls.EmptyPage; 
        public PagesUserControls PageToOpen { get => this._pageToOpen; set => this._pageToOpen = value; }

        /// <summary>
        /// Shows the bookmark in administrator page for which the button corresponds.
        /// </summary>
        private BookmarkInPage _bookmark;
        public BookmarkInPage Bookmark { get => _bookmark; set => _bookmark = value; }


        public int SelectedValue;

        public bool IsSelected
        {
            set
            {
                ButtonSelectedArgs isSelectedArg;
                this._isSeleceted = value;

                if (this._isSeleceted)
                {
                    isSelectedArg = new ButtonSelectedArgs(true);
                    IsSelectedEvent(this, isSelectedArg);
                }
                else
                {
                    isSelectedArg = new ButtonSelectedArgs(false);
                    IsSelectedEvent(this, isSelectedArg);
                }
            }
        }
        #endregion

        #region BasciConstructor
        public NavigationButtonTemplate()
        {
            InitializeComponent();

            this.IsSelectedEvent += NavigationButtonTemplate_IsSelectedEvent;
        }

        private void NavigationButtonTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_isSeleceted) ChangeColoursIsnSelected();
            else  ChangeColoursIsSelected();
        }

        private void NavigationButtonTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_isSeleceted) ChangeColoursIsSelected();
            else ChangeColoursIsnSelected();
           
        }
        #endregion

        #region NavigationButtonTemplate_IsSelectedEvent
        /// <summary>
        /// Action after button clicked.
        /// </summary>
        /// <param name="button">Current button.</param>
        /// <param name="isSelectedArg">Is selected argument, true - button is selected, false - isn't selected.</param>
        private void NavigationButtonTemplate_IsSelectedEvent(NavigationButtonTemplate button, ButtonSelectedArgs isSelectedArg)
        {
            if (_isSeleceted) ChangeColoursIsSelected();
            else ChangeColoursIsnSelected();
        }
        #endregion

        //TODO:Make this in xaml
        private void ChangeColoursIsSelected()
        {
            this.Background = this.FindResource("BackgroundOfPagesBrash") as Brush;
            this.Foreground = this.FindResource("FontsBrash") as Brush;
        }

        private void ChangeColoursIsnSelected()
        {
            this.Background = this.FindResource("EnabledBrash") as Brush;
            this.Foreground = this.FindResource("BackgroundOfPagesBrash") as Brush;
        }

        #region Button_Loaded
        /// <summary>
        /// Checks if the button can be seen by the current user.
        /// </summary>
        /// <param name="sender">Button that open a specific page</param>
        /// <param name="e">Button is loaded event</param>
        private void Button_Loaded(object sender, RoutedEventArgs e) => CheckIsVisible();
        #endregion

        #region CheckIsVisible
        /// <summary>
        /// Checks if current button can be seen by the currently logged in user.
        /// </summary>
        public void CheckIsVisible()
        {
            bool pageCanBeOpen = false;

            if (PageToOpen != PagesUserControls.EmptyPage)
            {
                if (LoggedInUser != null && PAGES_FOR_USER.TryGetValue(LoggedInUser.Permissions, out List<PagesUserControls> listWithPermissions))
                {
                    foreach (PagesUserControls puc in listWithPermissions)
                    {
                        if (puc == this.PageToOpen)
                        {
                            pageCanBeOpen = true;
                            break;
                        }
                    }
                }


                if (pageCanBeOpen)
                    this.Visibility = Visibility.Visible;

                else
                    this.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
