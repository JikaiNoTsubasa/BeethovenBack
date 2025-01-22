using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Database.DTO.TicketModels;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO;

public static class DTOHelper
{

#region User
    public static ResponseUser ToDTO(this User model){
        return new ResponseUser{
            Id = model.Id,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Name = model.Firstname + " " + model.Lastname,
            Email = model.Email,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById
        };
    }
#endregion
#region Customer
    public static ResponseCustomer ToDTO(this Customer model){
        return new ResponseCustomer{
            Id = model.Id,
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Comment = model.Comment,
            Tickets = model.Tickets?.Select(t=>t.ToDTO()).ToList(),
            Products = model.Products?.Select(p=>p.ToDTO()).ToList()
        };
    }
#endregion
#region Ticket
    public static ResponseTicket ToDTO(this Ticket model){
        return new ResponseTicket{
            Id = model.Id,
            Name = model.Name
        };
    }

    public static ResponseTicketStatus ToDTO(this TicketStatus model){
        return new ResponseTicketStatus{
            Id = model.Id,
            Name = model.Name
        };
    }
#endregion
#region Product
    public static ResponseProduct ToDTO(this Product model){
        return new ResponseProduct{
            Id = model.Id,
            Name = model.Name
        };
    }
#endregion
}