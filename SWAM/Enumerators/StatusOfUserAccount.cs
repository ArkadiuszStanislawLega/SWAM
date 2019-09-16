using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SWAM.Enumerators
{
    /// <summary>
    /// Status of users accounts.
    /// </summary>
    public enum StatusOfUserAccount
    {
        /// <summary>
        /// When the account is active.
        /// </summary>
        Active,
        /// <summary>
        /// When the account is temporary blocked.
        /// </summary>
        Blocked,
    }
}