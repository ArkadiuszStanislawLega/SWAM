using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// This exception may occur if an unexpected error occurs while refreshing the user's email list.
    /// </summary>
    public class RefreshUserEmailListException : BasicException
    {
        public RefreshUserEmailListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_EMAILS_LIST_ERROR}") { }
    }
}
