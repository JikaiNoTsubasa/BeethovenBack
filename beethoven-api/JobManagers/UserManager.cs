using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Global.Security;
using beethoven_api.JobManagers;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Managers;

public class UserManager(BeeDBContext context) : BeeManager(context)
{
    private IQueryable<User> GenerateUserQuery()
    {
        return _context.Users
            .Include(u => u.Teams);
    }

    public List<User> FetchAllUsers()
    {
        return [.. GenerateUserQuery().Where(u => u.IsDeleted == false)];
    }

    public User FetchUser(long id)
    {
        return GenerateUserQuery().FirstOrDefault(u => u.Id == id) ?? throw new Exception("User not found");
    }

    public User CreateUser(string firstname, string lastname, long userId, string? email = null, string? password = null, string? avatar = null)
    {
        User user = new()
        {
            Firstname = firstname,
            Lastname = lastname,
            Name = firstname + " " + lastname,
            Email = email,
            Password = BeeHash.GetHash(password ?? "test"),
            Avatar = avatar
        };
        user.MarkCreated(userId);
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void DeleteUser(long id, long userId)
    {
        User user = _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("User not found");
        user.MarkDeleted(userId);
        _context.SaveChanges();
    }

    public User UpdateUser(long id, long userId, string? firstname = null, string? lastname = null, string? email = null, string? password = null, string? avatar = null)
    {
        User user = _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("User not found");
        if (firstname is not null) user.Firstname = firstname;
        if (lastname is not null) user.Lastname = lastname;
        if (email is not null) user.Email = email;
        if (password is not null) user.Password = BeeHash.GetHash(password);
        if (avatar is not null) user.Avatar = avatar;
        user.MarkUpdated(userId);
        _context.SaveChanges();
        return user;
    }
}
