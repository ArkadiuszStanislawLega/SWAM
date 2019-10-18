namespace SWAM.Strings
{
    /// <summary>
    /// The class contains all enum types whose names are to be displayed in the application in the Polish language version.
    /// </summary>
    public class PLstrings
    {
        #region StatusOfUserAccount
        public const string BLOCK = "zablokowane";
        public const string ACTIVE = "aktywne";
        #endregion
        #region UserType
        public const string ADMINISTRATOR = "administrator";
        public const string WAREHOUSEMAN = "magazynier";
        public const string SELLER = "sprzedawca";
        public const string PROGRAMMER = "programista";
        public const string OWNER = "właściciel";
        public const string MENAGER = "kierownik";
        #endregion
        #region Customer order status
        public const string DELIVERED = "dostarczono";
        public const string IN_DELIVERY = "w dostawie";
        public const string WAITING_FOR_PAYMENT = "oczekiwanie na zapłate";
        public const string IN_PROCESS = "w przygotowaniu";
        #endregion
        #region ShipmentType
        public const string SHIPMENT = "kurier";
        public const string RECEPTION = "odbiór osobisty";
        #endregion
        #region PaymentType
        public const string PREPAID = "przedpłata";
        public const string POSTPAID = "płatne przy odbiorze";
        #endregion
    }
}
