using System.ComponentModel.DataAnnotations;
using EmployeeManagement.DTOs.SharedModel;

namespace EmployeeManagement.Repositories.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EnumDataType(typeof(UserRoles))]
        public UserRoles Role { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
