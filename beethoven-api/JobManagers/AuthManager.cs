using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.Global.Security;
using beethoven_api.Global.Token;

namespace beethoven_api.JobManagers;

public class AuthManager(BeeDBContext context) : BeeManager(context)
{
    public virtual ResponseLogin LoginUser(string email, string password){
        User? user = _context.Users.FirstOrDefault(u=>u.Email!.Equals(email));
        if (user is null) return new(){ IsLogged = false };
        
        if (user.IsDeleted || !user.CanLogin) return new(){ IsLogged = false };

        if (BeeHash.ValidateHash(password, user.Password!))
        {
            var bearer = AuthorizationToken.GenerateBearer(user, AuthConstants.JwtSecret);
            user.Lastconnection = DateTime.UtcNow;
            _context.SaveChanges();

            return new()
            {
                User = user?.ToDTO(),
                AccessToken = bearer,
                IsLogged = true
            };

        }
        return new(){ IsLogged = false };
    }
}
