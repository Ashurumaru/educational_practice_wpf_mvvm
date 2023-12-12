using System.ComponentModel.DataAnnotations;

namespace educational_practice.Models
{
    internal class UserModel
    {
        [Key] public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int AccessLevel { get; set; } 
    }
}
