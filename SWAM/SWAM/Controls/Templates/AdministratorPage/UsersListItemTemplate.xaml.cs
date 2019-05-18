using SWAM.Events.NavigationButton;
using SWAM.Models;
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

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy UsersListItem.xaml
    /// </summary>
    public partial class UsersListItemTemplate : Button
    {
        public delegate void SelectedHandler(UsersListItemTemplate button, ButtonSelectedArgs isSelectedArg);
        public event SelectedHandler IsSelectedEvent;

        #region Properties
        /// <summary>
        /// Flag indicating whether the button is pressed.
        /// </summary>
        private bool _isSeleceted;

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
        #region Basic constructor
        public UsersListItemTemplate()
        {
            InitializeComponent();

            this.IsSelectedEvent += UsersListItemTemplate_IsSelectedEvent;
        }
        #endregion
        #region UsersListItemTemplate_IsSelectedEvent
        /// <summary>
        /// Action after button is selected.
        /// </summary>
        /// <param name="button">Current UsersListItemTemplate</param>
        /// <param name="isSelectedArg"></param>
        private void UsersListItemTemplate_IsSelectedEvent(UsersListItemTemplate button, ButtonSelectedArgs isSelectedArg)
        {
            if (this._isSeleceted)
            {
                this.Background = this.FindResource("BackgroundOfPagesBrash") as Brush;
                this.UserRole.Foreground = this.FindResource("FontsBrash") as Brush;
            }
            else
            {
                this.Background = this.FindResource("EnabledBrash") as Brush;
                this.UserRole.Foreground = this.FindResource("DarkGraphiteBrash") as Brush;
            }
        }
        #endregion
    }
}
