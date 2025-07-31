using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.Database.DTO.TeamModels;
using beethoven_api.Database.DTO.UserModels;
using beethoven_api.Global.Security;
using beethoven_api.Global.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace beethoven_api.Global.Engine;

public class BeeEngine(BeeDBContext context) 
{
    protected BeeDBContext _context = context;

    public virtual User CreateUser(RequestCreateUser model, long userId){
        User user = new(){
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Name = model.Firstname + " " + model.Lastname,
            Email = model.Email,
            Password = BeeHash.GetHash(model.Password ?? "test"),
            Avatar = model.Avatar
        };
        user.MarkCreated(userId);
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public virtual Customer CreateCustomer(RequestCreateCustomer model, long userId){
        Customer customer = new(){
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Comment = model.Comment
        };
        customer.MarkCreated(userId);
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public virtual Team CreateTeam(RequestCreateTeam model, long userId){
        Team team = new(){
            Name = model.Name,
        };
        team.MarkCreated(userId);
        _context.Teams.Add(team);
        _context.SaveChanges();
        return team;
    }

    public virtual Team AddMemberToTeam(long teamId, long userId){
        Team team = _context.Teams.Include(t=>t.Members).FirstOrDefault(t=>t.Id == teamId) ?? throw new Exception("Team not found");
        User user = _context.Users.Include(u=>u.Teams).FirstOrDefault(u=>u.Id == userId) ?? throw new Exception("User not found");
        team.Members!.Add(user);
        _context.SaveChanges();
        return team;
    }

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
