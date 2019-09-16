using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// Exception when there is a problem when application want to refresh user profile in administrator page.
    /// </summary>
    public class RefreshUserListException : BasicException
    {
        public RefreshUserListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_USER_LIST_ERROR}") { }
    }
}
