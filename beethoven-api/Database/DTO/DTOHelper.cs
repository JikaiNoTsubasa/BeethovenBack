using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO;

public static class DTOHelper
{
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
}