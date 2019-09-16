using SWAM.Strings;

namespace SWAM.Exceptions
{
    /// <summary>
    /// This exception may occur if an unexpected error occurs while refreshing the user warehousess accesses list.
    /// </summary>
    public class RefreshWarehousessAccessesListExeption : BasicException
    {
        public RefreshWarehousessAccessesListExeption(string message = "") : base($"{message} {ErrorMesages.REFRESH_WAREHOUSES_ACCESSES_LIST_ERROR}")
        {}
    }
}
