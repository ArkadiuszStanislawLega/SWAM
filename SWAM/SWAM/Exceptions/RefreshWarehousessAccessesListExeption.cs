using SWAM.Strings;

namespace SWAM.Exceptions
{
    public class RefreshWarehousessAccessesListExeption : BasicException
    {
        public RefreshWarehousessAccessesListExeption(string message = "") : base($"{message} {ErrorMesages.REFRESH_WAREHOUSES_ACCESSES_LIST_ERROR}")
        {}
    }
}
