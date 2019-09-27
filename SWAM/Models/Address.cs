namespace SWAM
{
    /// <summary>
    /// The basic class model in the database representing the address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Number Id in database.
        /// </summary>
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }
    }
}
