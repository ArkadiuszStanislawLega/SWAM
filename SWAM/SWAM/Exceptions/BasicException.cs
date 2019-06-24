using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Exceptions
{
    public abstract class BasicException : Exception
    {
        public BasicException(string message) : base (message) { }
        public void ShowMessage(UserControl content)
        {
            if(!InformationToUser(content))
                MessageBox.Show(this.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        private bool InformationToUser(UserControl content)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(content) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(this.Message, true);
                    return true;
                }
                else throw new InformationLabelException(this.Message);
            }
            catch (InformationLabelException)
            {
                return false;
            }
        }
        #endregion
    }
}
