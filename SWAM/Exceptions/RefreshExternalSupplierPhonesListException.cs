﻿using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// This exception may occur if an unexpected error occurs while refreshing the user's phones list.
    /// </summary>
    public class RefreshExternalsupplierPhonesListException : BasicException
    {
        public RefreshExternalsupplierPhonesListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_PHONES_LIST_ERROR}") { }
    }
}
