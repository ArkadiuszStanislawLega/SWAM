using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// This exception may occur if an unexpected error occurs while refreshing the user's profile.
    /// </summary>
    public class RefreshUserProfileException : BasicException
    {
        public RefreshUserProfileException(string message = "") :
            base($"{message} {ErrorMesages.REFRESH_USER_PROFILE_ERROR}") { }
    }
}
