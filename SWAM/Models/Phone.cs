
namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the telephone number.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Identification number from the phone number database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Note regarding the telephone number.
        /// </summary>
        public string Note { get; set; }
        public override string ToString() => $"{this.Note} - {this.PhoneNumber}";
    }
}
