namespace beethoven_api.Database.DTO.LoginModels;

public record RequestLogin
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
