namespace DataWise.Common.DTOs;

public class UpdateProfileDto
{
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}