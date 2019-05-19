using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SWAM.Enumerators;

namespace SWAM
{
    public abstract class Page : UserControl
    {
        #region Properties
        /// <summary>
        /// Name of the current page.
        /// </summary>
        protected const PagesUserControls NAME_OF_PAGE = PagesUserControls.BasicPage;
        /// <summary>
        /// Instance of main window.
        /// </summary>
        protected MainWindow _mainWindow;
        #endregion

        #region BasicConstructor

        public Page(MainWindow mainWindow)
        {
            this._mainWindow = mainWindow;
            this._mainWindow.CurrentPageLoaded = NAME_OF_PAGE;
        }
        #endregion

        #region ChangePage_Click
        /// <summary>
        /// Use it, when work on this page is finish and u want switch current page to another.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        abstract protected void ChangePage_Click(object sender, RoutedEventArgs e);
        #endregion
    }
}
