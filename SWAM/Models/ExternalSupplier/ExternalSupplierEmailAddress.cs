namespace SWAM.Models.ExternalSupplier
{
    public class ExternalSupplierEmailAddress
    {
        /// <summary>
        /// Email address number Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Person's email address.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Note to email address.
        /// </summary>
        public string Note { get; set; }
        public ExternalSupplier ExternalSupplier { get; set; }
        public int ExternalSupplierId { get; set; }

    }
}
