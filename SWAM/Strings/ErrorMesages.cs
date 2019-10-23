namespace SWAM.Strings
{
    public static class ErrorMesages
    {
        public const string INFORMATION_LABEL_ERROR = "Wystąpił błąd, nie znaleziono paska informacyjnego.";
        public const string DATACONTEXT_ERROR = "Wystąpił problem z DataContext widoku.";
        public const string MESSAGE_WINDOW_ERROR = "Wystąpił problem z oknem zapytania.";
        public const string DATABASE_ERROR = "Wystąpił problem z bazą danych.";
        public const string CANCEL_ERROR = "W czasie anulowania wystąpił błąd.";
        public const string CONFIRMATION_WINDOW_ERROR = "Wystąpił problem z oknem potwierdzenia.";

        public const string PASSWORD_REQUIREMENT_ERROR = "Hasło nie spełnia wymagań. Musi posiadać 8 znaków, conajmniej jeden z wielkiej litery oraz conajmniej jedną cyfrę.";
        public const string PASSWORD_SALT_NULL_EXCEPTION = "Password salt jest pusty, prawdopodobnie w czasie tworzenia konta wystąpił błąd przy generowaniu soli hasła.";
        public const string PASSWORD_SALT_NULL_EXCEPTION_TIP = "Zgłoś się do administratora systemu hasło konta musi zostać przydzielone jeszcze raz w panelu administracyjnym.";
        #region User
        public const string REFRESH_USER_PROFILE_ERROR = "Wystąpił błąd odświerzania profilu użytkownika.";
        public const string REFRESH_USER_LIST_ERROR = "Wystąpił błąd podczas odświerzania listy użytkowników.";
        public const string DURING_CHANGING_STATUS_USER_ACCOUT_ERROR = "Wystąpił błąd podczas zmiany statusu konta użytkownika.";
        public const string DURING_EDIT_USER_ERROR = "Wystąpił błąd podczas edytowania danych użytkownika.";
        public const string DURING_DELETE_USER_ERROR = "Wystąpił błąd podczas usuwania użytkownika.";
        public const string DURING_CHANGING_USER_PROFILE_ERROR = "Wystąpił błąd przełączania profilu użytkownika.";
        public const string DURING_EDIT_PASSWORD_ERROR = "Wystąpił błąd podczas edytowania hasła użytkownika.";
        #endregion
        #region User accesses to warehouses
        public const string REFRESH_WAREHOUSES_ACCESSES_LIST_ERROR = "Wystąpił błąd podczas odświerzania listy dostępu do magazynów.";
        public const string DURING_EDIT_ACCESS_TO_WAREHOUSE_ERROR = "Wystąpił błąd podczas edytowania uprawnienia użytkownika do magazynu.";
        public const string DURING_ADD_ACCESS_TO_WAREHOUSE_ERROR = "Wystąpił błąd podczas dodawania nowego uprawnienia użytkownika od magazynu.";
        public const string DURING_DELETE_ACCESS_TO_WAREHOUSE_ERROR = "Wystąpił błąd podczas usuwania uprawnienia użytkownika od magazynu.";
        #endregion
        #region Email
        public const string REFRESH_EMAILS_LIST_ERROR = "Wystąpił błąd podczas wczytywania listy adresów email użytkownika.";
        public const string DURIGN_DELETE_EMAIL_ERROR = "Wystąpił błąd podczas usuwania adresu email.";
        public const string DURING_EDIT_EMAIL_ERROR = "Wystąpił błąd podczas edytowania adresu email.";
        #endregion
        #region Phone
        public const string REFRESH_PHONES_LIST_ERROR = "Wystąpił błąd podczas wczytywania listy telefonów użytkownika.";
        public const string DURING_DELETE_PHONE_ERROR = "Wystąpił błąd podczas usuwania numeru telefonu.";
        public const string DURING_ADD_PHONE_ERROR = "Wystąpił błąd podczas dodawania nowego numeru telefonu.";
        public const string DURING_EDIT_PHONE_ERROR = "Wystąpił błąd podczas edytowania numeru telefonu.";
        #endregion
        #region Products
        public const string DURING_DELETE_PRODUCT_ERROR = "Wystąpił błąd podczas usuwania produktu.";
        public const string DURING_ADD_PRODUCT_ERROR = "Wystąpił błąd podczas dodawania produktu.";
        public const string DURING_EDIT_PRODUCT_ERROR = "Wystąpił błąd podczas edytowania produktu.";
        #endregion
        #region Warehouse
        public const string REFRESH_WAREHOUSES_LIST_ERROR = "W czasie odświerzania listy magazynów.";
        public const string DURGIN_DELETE_WAREHOUSE_ERROR = "Wystąpił błąd podczas usuwania magazynu.";
        public const string DURGIN_ADD_WAREHOUSE_ERROR = "Wystąpił błąd podczas dodawania nowego magazynu.";
        #endregion
        #region Message
        public const string MESSAGE_READ_ERROR = "Wystąpił błąd wczytywania wiadomości.";
        #endregion
        #region Customer
        public const string REFRESH_CUSTOMER_PROFILE_ERROR = "Wystąpił błąd odświerzania profilu klienta.";
        #endregion
        #region External supplier
        public const string REFRESH_EXTERNAL_SUPPLIER_PROFILE_ERROR = "Wystąpił błąd odświerzania profilu zewnętrznego dostawcy.";
        public const string EDIT_EXTERNAL_SUPPLIER_PHONE_ERROR = "Wystąpił błąd podczas edycji numeru telefonu kontaktowego dostawcy zewnętrzenego.";
        public const string ADD_EXTERNAL_SUPPLIER_PHONE_ERROR = "Wystąpił błąd podczas dodawania numeru telefonu kontaktowego dostawcy zewnętrzenego.";
        public const string DELETE_EXTERNAL_SUPPLIER_PHONE_ERROR = "Wystąpił błąd podczas usuwania numeru telefonu kontaktowego dostawcy zewnętrzenego.";
        public const string EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR = "Wystąpił błąd podczas edycji adresu dostawcy zewnętrzenego.";
        public const string CANCEL_EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR = "Wystąpił błąd podczas anulowania edycji adresu dostawcy zewnętrzenego.";
        public const string EDIT_EXTERNAL_SUPPLIER_PROPER_NAME_ERROR = "Wystąpił błąd podczas edycji nazwy własnej dostawcy zewnętrzenego.";
        public const string EDIT_EXTERNAL_SUPPLIER_TIN_ERROR = "Wystąpił błąd podczas edycji TIN dostawcy zewnętrzenego.";
        #endregion
    }
}