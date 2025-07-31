namespace beethoven_api.Database.DTO.UserModels;

public record RequestUpdateUser
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Avatar { get; set; }
}
