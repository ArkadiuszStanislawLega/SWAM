using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SWAM.Exceptions
{
    public abstract class BasicException : Exception
    {
        public BasicException(string message) : base (message) { }
        public void ShowMessage() => MessageBox.Show(this.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
