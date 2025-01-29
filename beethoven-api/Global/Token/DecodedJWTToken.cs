using System;

namespace beethoven_api.Global.Token;

public class DecodedJWTToken
{
    public string? Kid { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public Dictionary<string, string>? Claims { get; set; }
}
