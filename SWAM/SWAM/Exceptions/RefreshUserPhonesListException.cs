using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// This exception may occur if an unexpected error occurs while refreshing the user's phones list.
    /// </summary>
    public class RefreshUserPhonesListException : BasicException
    {
        public RefreshUserPhonesListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_EMAILS_LIST_ERROR}") { }
    }
}
