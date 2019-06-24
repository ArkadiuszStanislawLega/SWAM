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
    /// When the method doesn't find label with information to user.
    /// </summary>
    public class InformationLabelException : BasicException
    {
        public InformationLabelException(string message) :
            base($"{message} {ErrorMesages.INFORMATION_LABEL_ERROR}")
        { }

        new public void ShowMessage(UserControl content)
        {
            MessageBox.Show(this.Message);
        }
    }
}
