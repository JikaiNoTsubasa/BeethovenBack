using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Database.DTO.SLAModels;
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
            DeletedById = model.DeletedById,
            Avatar = model.Avatar
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
            Products = (model.Products is not null && model.Products.Count != 0) ? model.Products?.Select(p=>p.ToDTO()).ToList() : []
        };
    }
    public static ResponseCustomerSimplified ToDTOSimplified(this Customer model){
        return new ResponseCustomerSimplified{
            Id = model.Id,
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Comment = model.Comment
        };
    }
#endregion
#region Ticket
    public static ResponseTicket ToDTO(this Ticket model){
        return new ResponseTicket{
            Id = model.Id,
            Name = model.Name,
            Status = model.Status?.ToDTO(),
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            AssignedTo = model.AssignedTo?.ToDTO(),
            ReviewedBy = model.ReviewedBy?.ToDTO(),
            Description = model.Description,
            Product = model.Product?.ToDTO(),
            GitlabTicketId = model.GitlabTicketId,
            Activities = (model.Activities is not null && model.Activities.Count != 0) ? model.Activities?.Select(a=>a.ToDTO()).ToList() : []
        };
    }

    public static ResponseTicketStatus ToDTO(this TicketStatus model){
        return new ResponseTicketStatus{
            Id = model.Id,
            Name = model.Name
        };
    }

    public static ResponseTicketActivity ToDTO(this TicketActivity model){
        return new ResponseTicketActivity{
            Id = model.Id,
            User = model.User?.ToDTO(),
            Message = model.Message,
            CreatedAt = model.CreatedAt
        };
    }
#endregion
#region Product
    public static ResponseProduct ToDTO(this Product model){
        return new ResponseProduct{
            Id = model.Id,
            Name = model.Name,
            SLA = model.SLA?.ToDTOSimplified(),
            Customer = model.Customer?.ToDTOSimplified(),
            CodeBaseLink = model.CodeBaseLink
        };
    }
#endregion
#region SLA
    public static ResponseSLA ToDTO(this SLA model){
        return new ResponseSLA{
            Id = model.Id,
            Name = model.Name,
            Products = (model.Products is not null && model.Products.Count != 0) ? model.Products?.Select(p=>p.ToDTO()).ToList() : []
        };
    }

    public static ResponseSLASimplified ToDTOSimplified(this SLA model){
        return new ResponseSLASimplified{
            Id = model.Id,
            Name = model.Name
        };
    }
#endregion
}