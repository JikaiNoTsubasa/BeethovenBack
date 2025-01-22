namespace beethoven_api.Database.DTO.UserModels;

public record class RequestUpdateUser
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
