using SWAM.Strings;

namespace SWAM.Exceptions
{
    public class RefreshUserPhonesListException : BasicException
    {
        public RefreshUserPhonesListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_EMAILS_LIST_ERROR}") { }
    }
}
