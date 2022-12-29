using System.ComponentModel.DataAnnotations;

namespace Ynventory.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
    }
}
