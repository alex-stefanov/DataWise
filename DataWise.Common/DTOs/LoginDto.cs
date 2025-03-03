using System.ComponentModel.DataAnnotations;

namespace DataWise.Common.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }

    public bool RememberMe { get; set; }
}