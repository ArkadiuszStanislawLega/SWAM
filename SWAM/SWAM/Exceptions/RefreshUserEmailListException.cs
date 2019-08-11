using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Strings;

namespace SWAM.Exceptions
{
    public class RefreshUserEmailListException : BasicException
    {
        public RefreshUserEmailListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_EMAILS_LIST_ERROR}") { }
    }
}
