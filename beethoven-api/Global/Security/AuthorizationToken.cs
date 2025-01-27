using System;
using System.Security.Claims;
using beethoven_api.Database.DBModels;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace beethoven_api.Global.Security;

public class AuthorizationToken
{
    public static string GenerateBearer(User user, string bearerSecret){
        // Create claims
        var claims = new[] { 
            new Claim("user.id", user.Id.ToString()),
            new Claim("user.email", user.Email!),
            new Claim("user.name", user.Name!),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(6).Ticks.ToString()),
        };

        // Create creds
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor 
        { 
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddHours(6), 
            SigningCredentials = creds
        };

        // Create token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        token.Header.Remove("typ");
        token.Header.Remove("alg");
        token.Header.Add("typ", "JWT");
        token.Header.Add("alg", "ES256");

        return tokenHandler.WriteToken(token);
    }
}
