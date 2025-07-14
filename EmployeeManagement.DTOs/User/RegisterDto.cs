using System.ComponentModel.DataAnnotations;
using EmployeeManagement.DTOs.SharedModel; 

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Username must contain only English letters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must contain upper, lower, number, special char and be 6+ characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role is required")]
    [EnumDataType(typeof(UserRoles), ErrorMessage = "Invalid role type")]
    public UserRoles Role { get; set; }
}
