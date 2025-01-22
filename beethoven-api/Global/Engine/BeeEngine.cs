using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.UserModels;
using beethoven_api.Global.Security;

namespace beethoven_api.Global.Engine;

public class BeeEngine(BeeDBContext context) 
{
    protected BeeDBContext _context = context;

    public virtual User CreateUser(RequestCreateUser model, long userId){
        User user = new(){
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            Password = BeeHash.GetHash(model.Password ?? "test"),
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
}
