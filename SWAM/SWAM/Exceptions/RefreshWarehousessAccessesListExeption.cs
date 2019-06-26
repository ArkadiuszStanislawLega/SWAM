using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Exceptions
{
    public class RefreshWarehousessAccessesListExeption : BasicException
    {
        public RefreshWarehousessAccessesListExeption(string message = "") : base($"{message} {ErrorMesages.REFRESH_WAREHOUSES_ACCESSES_LIST_ERROR}")
        {}
    }
}
