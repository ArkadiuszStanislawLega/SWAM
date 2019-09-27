
namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing the <see cref="Address"/> of the <see cref="SWAM.Models.Person"/>.
    /// </summary>
    public class PersonAddresses : Address
    {
        /// <summary>
        /// The person to whom the address belongs.
        /// </summary>
        public Person Person { get; set; }
    }
}
