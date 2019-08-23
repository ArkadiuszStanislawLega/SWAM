using System.Windows;
using SWAM.Strings;

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

        new public void ShowMessage(FrameworkElement content) => MessageBox.Show(this.Message);
    }
}
