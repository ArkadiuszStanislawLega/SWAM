using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWAM.Strings;

namespace SWAM.Exceptions
{
    public class RefreshUserProfileException : BasicException
    {
        public RefreshUserProfileException(string message = "") :
            base($"{message} {ErrorMesages.REFRESH_USER_PROFILE_ERROR}") { }
    }
}
