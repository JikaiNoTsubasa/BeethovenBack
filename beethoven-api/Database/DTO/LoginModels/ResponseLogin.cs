using System;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.LoginModels;

public record ResponseLogin
{
    public ResponseUser? User { get; set; }
    public string? AccessToken { get; set; }
    public bool IsLogged { get; set; } = false;
}
