
namespace beethoven_api.Database.DTO.UserModels;

public record ResponseUser : ResponseEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public bool CanLogin { get; set; }
    public DateTime? LastConnection { get; set; }
}