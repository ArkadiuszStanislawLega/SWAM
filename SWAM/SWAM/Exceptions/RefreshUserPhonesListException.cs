﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Exceptions
{
    public class RefreshUserPhonesListException : BasicException
    {
        public RefreshUserPhonesListException(string message = "") : base($"{message} {ErrorMesages.REFRESH_EMAILS_LIST_ERROR}") { }
    }
}
