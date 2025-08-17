namespace beethoven_api.Database.DTO.ProjectModels;

public record class ResponseProjectPermission
{
    public long UserId { get; set; }
    public long ProjectId { get; set; }
    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanCreateTasks { get; set; }
    public bool CanConfigure { get; set; }
}
