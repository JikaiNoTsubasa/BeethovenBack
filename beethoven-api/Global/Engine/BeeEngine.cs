using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Database.DTO.TicketModels;
using beethoven_api.Database.DTO.UserModels;
using beethoven_api.Global.Security;
using beethoven_api.Global.Token;

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

    public virtual Product CreateProduct(RequestCreateProduct model, long userId){
        Product product = new(){
            Name = model.Name,
            SLAId = model.SLAId,
            CustomerId = model.CustomerId
        };
        product.MarkCreated(userId);
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public virtual Ticket CreateTicket(RequestCreateTicket model, long userId){
        Ticket ticket = new(){
            Name = model.Name,
            Description = model.Description,
            ProductId = model.ProductId,
            AssignedToId = model.AssignedToId,
            ReviewedById = model.ReviewedById,
            GitlabTicketId = model.GitlabTicketId,
            TypeId = model.TypeId,
            PriorityId = model.PriorityId
        };
        if (model.StatusId is not null){
            ticket.StatusId = model.StatusId;
        }else if (model.AssignedToId is not null){
            ticket.StatusId = 2;
        }else{
            ticket.StatusId = 1;
        }
        ticket.MarkCreated(userId);

        TicketActivity activity = new(){
            TicketId = ticket.Id,
            UserId = userId,
            Message = "Ticket created",
            CreatedAt = DateTime.UtcNow  
        };
        ticket.Activities = [];
        ticket.Activities.Add(activity);
        _context.Tickets.Add(ticket);
        _context.SaveChanges();
        return ticket;
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
