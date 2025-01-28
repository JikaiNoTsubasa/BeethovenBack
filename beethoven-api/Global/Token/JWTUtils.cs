using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace beethoven_api.Global.Token;

public class JWTUtils
{
    public static string GenerateJWTToken(Claim[] claims, string secret) {
        // Create creds
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddDays(1);

        var token = new JwtSecurityToken(
            issuer: AuthConstants.JwtIssuer,
            audience: AuthConstants.JwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static Claim[] GetClaims(long id, string email) {
        return [ 
            new Claim("user.id", id.ToString()),
            new Claim("user.email", email!)
        ];
    }
}
