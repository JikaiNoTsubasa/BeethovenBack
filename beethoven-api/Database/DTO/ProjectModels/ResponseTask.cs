using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.ProjectModels;

public record class ResponseTask : ResponseEntity
{
    public string? Description { get; set; }

    public List<ResponseUser>? Assignees { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int EstimatedMinutes { get; set; }
    public Priority Priority { get; set; }
    public DBModels.TaskStatus Status { get; set; }
    public long? PhaseId { get; set; }
    public long? ProjectId { get; set; }
}
