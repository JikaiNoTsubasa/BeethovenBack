using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DTO.UserModels;

public record RequestCreateUser{
    [Required]
    public string Firstname { get; set; } = null!;
    [Required]
    public string Lastname { get; set; } = null!;
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Avatar { get; set; }
}