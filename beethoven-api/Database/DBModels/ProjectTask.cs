using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class ProjectTask : Entity
{
    public string? Description { get; set; }

    public List<User>? Assignees { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int EstimatedMinutes { get; set; }
    public Priority Priority { get; set; } = Priority.LOW;
    public TaskStatus Status { get; set; } = TaskStatus.TODO;
    public TaskType Type { get; set; }

    [ForeignKey(nameof(Project))]
    public long? ProjectId { get; set; }
    public Project? Project { get; set; }

    [ForeignKey(nameof(Phase))]
    public long? PhaseId { get; set; }
    public ProjectPhase? Phase { get; set; }

    [ForeignKey(nameof(Document))]
    public long? ParentTaskId { get; set; }
    public ProjectTask? ParentTask { get; set; }

    public List<ProjectTask>? SubTasks { get; set; }
}
