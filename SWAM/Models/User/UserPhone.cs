using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.User
{
    [Table("UsersPhones")]
    public class UserPhone : Phone
    {
        public User User { get; set; }
    }
}
