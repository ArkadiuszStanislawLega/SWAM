using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.User
{
    [Table("UsersEmailAddresses")]
    public class UserEmailAddress : EmailAddress
    {
        public User User { get; set; }
    }
}
