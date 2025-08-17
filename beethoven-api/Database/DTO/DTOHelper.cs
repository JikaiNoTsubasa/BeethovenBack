using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.DocumentModel;
using beethoven_api.Database.DTO.MessageModels;
using beethoven_api.Database.DTO.ProjectModels;
using beethoven_api.Database.DTO.TeamModels;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO;

public static class DTOHelper
{

    #region User
    public static ResponseUser ToDTO(this User model)
    {
        return new ResponseUser
        {
            Id = model.Id,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Name = model.Firstname + " " + model.Lastname,
            Email = model.Email,
            CanLogin = model.CanLogin,
            LastConnection = model.Lastconnection,
            IsDeleted = model.IsDeleted,
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
    public static ResponseCustomer ToDTO(this Customer model)
    {
        return new ResponseCustomer
        {
            Id = model.Id,
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Comment = model.Comment
        };
    }
    public static ResponseCustomerSimplified ToDTOSimplified(this Customer model)
    {
        return new ResponseCustomerSimplified
        {
            Id = model.Id,
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            ContactPhone = model.ContactPhone,
            Comment = model.Comment
        };
    }
    #endregion
    #region Team
    public static ResponseTeam ToDTO(this Team model)
    {
        return new ResponseTeam
        {
            Id = model.Id,
            Name = model.Name,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById,
            Members = (model.Members is not null && model.Members.Count != 0) ? model.Members?.Select(u => u.ToDTO()).ToList() : []
        };
    }

    public static ResponseTeamSimplified ToDTOSimplified(this Team model)
    {
        return new ResponseTeamSimplified
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById,
            Name = model.Name
        };
    }
    #endregion

    #region Message
    public static ResponseMessage ToDTO(this Message model)
    {
        return new ResponseMessage
        {
            Id = model.Id,
            Text = model.Text,
            IsRead = model.IsRead,
            SourceUser = model.SourceUser.ToDTO(),
            TargetUser = model.TargetUser.ToDTO(),
            CreatedAt = model.CreatedAt
        };
    }
    #endregion

    #region Project
    public static ResponseProject ToDTO(this Project model)
    {
        return new ResponseProject
        {
            Id = model.Id,
            Name = model.Name,
            OwnerId = model.OwnerId,
            OwnerName = model.Owner?.Name,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById,
            CustomerId = model.CustomerId,
            CustomerName = model.Customer?.Name,
            CurrentPhaseId = model.CurrentPhaseId
        };
    }

    public static ResponseProjectPhase ToDTO(this ProjectPhase model)
    {
        return new ResponseProjectPhase
        {
            Id = model.Id,
            Name = model.Name,
            PreviousPhaseId = model.PreviousPhaseId,
            NextPhaseId = model.NextPhaseId
        };
    }

    public static ResponseProjectPermission ToDTO(this ProjectPermission model)
    {
        return new ResponseProjectPermission
        {
            UserId = model.UserId,
            ProjectId = model.ProjectId,
            CanRead = model.CanRead,
            CanUpdate = model.CanUpdate,
            CanCreateTasks = model.CanCreateTasks,
            CanConfigure = model.CanConfigure
        };
    }
    #endregion

    #region Tasks

    public static ResponseTask ToDTO(this DBModels.ProjectTask model)
    {
        return new ResponseTask
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Priority = model.Priority,
            Status = model.Status,
            ProjectId = model.ProjectId,
            Assignees = model.Assignees?.Select(u => u.ToDTO()).ToList(),
            EstimatedMinutes = model.EstimatedMinutes,
            PhaseId = model.PhaseId,

            IsDeleted = model.IsDeleted,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById
        };
    }

    #endregion

    #region Document
    public static ResponseDocument ToDTO(this Document model)
    {
        return new ResponseDocument
        {
            Id = model.Id,
            Name = model.Name,
            Version = model.CurrentVersion.Version,
            VersionId = model.CurrentVersion.Id,
            Path = model.CurrentVersion.Path,
            IsDeleted = model.IsDeleted,
            CreatedAt = model.CreatedAt,
            CreatedById = model.CreatedById,
            UpdatedAt = model.UpdatedAt,
            UpdatedById = model.UpdatedById,
            DeletedAt = model.DeletedAt,
            DeletedById = model.DeletedById,
            EntityId = model.EntityId,
            EntityName = model.Entity?.Name
        };
    }
    #endregion
}