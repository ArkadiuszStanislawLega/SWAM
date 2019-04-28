using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SWAM
{
    public abstract class Page : UserControl
    {
        #region Properties
        /// <summary>
        /// Name of the current page.
        /// </summary>
        protected const Pages NAME_OF_PAGE = Pages.basicPage;
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

        abstract protected void ChangePage_Click(object sender, RoutedEventArgs e);
    }
}
