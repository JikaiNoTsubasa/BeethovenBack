using System;
using System.Security.Claims;
using beethoven_api.Database.DBModels;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using beethoven_api.Global.Token;

namespace beethoven_api.Global.Security;

public class AuthorizationToken
{
    public static string GenerateBearer(User user, string bearerSecret){
        // Create claims
        var claims = new[] { 
            new Claim("user.id", user.Id.ToString()),
            new Claim("user.email", user.Email!),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.Ticks.ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(6).Ticks.ToString()),
        };

        return JWTUtils.GenerateJWTToken(claims, bearerSecret);
    }
}
