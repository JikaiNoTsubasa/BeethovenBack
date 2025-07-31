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
        string hashedPassword = BeeHash.GetHash(password);
        User? user = _context.Users.FirstOrDefault(u=>u.Email == email && u.Password!.Equals(hashedPassword));


        bool isLogged = user is not null;

        if (user is null) return new(){ IsLogged = false };

        var bearer = AuthorizationToken.GenerateBearer(user, AuthConstants.JwtSecret);

        return new(){
            User = user?.ToDTO(),
            AccessToken = bearer,
            IsLogged = isLogged
            };
    }
}
