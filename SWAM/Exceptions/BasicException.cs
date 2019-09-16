using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Exceptions
{
    /// <summary>
    /// The basic class that inherits from Exceptions is compatible with the application GUI.
    /// </summary>
    public abstract class BasicException : Exception
    {
        public BasicException(string message) : base (message) { }
        /// <summary>
        /// Displays information for the user.
        /// </summary>
        /// <param name="content">FrameworkElement during which an exception occurred.</param>
        public void ShowMessage(FrameworkElement content)
        {
            if(!InformationToUser(content))
                MessageBox.Show(this.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region InformationToUser
        /// <summary>
        /// Changing content information label in main window.
        /// </summary>
        /// <param name="content">FrameworkElement during which an exception occurred.</param>
        /// <returns>False - when the parent was not found, true - if the parent was found and the information was displayed. </returns>
        private bool InformationToUser(FrameworkElement content)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(content) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser($"{this.Message} {content.GetType().ToString()}", true);
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
