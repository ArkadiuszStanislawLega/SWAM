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
    /// <summary>
    /// Logika interakcji dla klasy NavigationButtonTemplate.xaml
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
        private PagesUserControls _pageToOpen = PagesUserControls.BasicPage; 

        public PagesUserControls PageToOpen { get => this._pageToOpen; set => this._pageToOpen = value; }

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
        #endregion

        #region NavigationButtonTemplate_IsSelectedEvent
        /// <summary>
        /// Action after button clicked.
        /// </summary>
        /// <param name="button">Current button.</param>
        /// <param name="isSelectedArg">Is selected argument, true - button is selected, false - isn't selected.</param>
        private void NavigationButtonTemplate_IsSelectedEvent(NavigationButtonTemplate button, ButtonSelectedArgs isSelectedArg)
        {
            if (_isSeleceted) this.Background = this.FindResource("BackgroundOfPagesBrash") as Brush;
            else this.Background = this.FindResource("MainBarBrash") as Brush;
        }
        #endregion
    }
}
