using SWAM.Strings;

namespace SWAM.Exceptions
{   
    /// <summary>
    /// This exception may appear if the account password salt was generated incorrectly when creating the account.
    /// </summary>
    public class PasswordSaltNullException : BasicException
    {
        public PasswordSaltNullException(string message = "") : base($"{message} {ErrorMesages.PASSWORD_SALT_NULL_EXCEPTION}") { }
    }
}
