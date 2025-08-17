using System.ComponentModel.DataAnnotations;
using beethoven_api.Database.DBModels;

namespace beethoven_api.Database.DTO.ProjectModels;

public record class RequestCreateTask
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<long>? AssigneeIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? EstimatedMinutes { get; set; }
    public Priority? Priority { get; set; }
    [Required]
    public long PhaseId { get; set; }
}
