using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class Task : Entity
{
    public string? Description { get; set; }

    public List<User>? Assignees { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int EstimatedMinutes { get; set; }
    public Priority Priority { get; set; } = Priority.LOW;
    public TaskStatus Status { get; set; } = TaskStatus.TODO;

    [ForeignKey(nameof(Phase))]
    public long? PhaseId { get; set; }
    public ProjectPhase? Phase { get; set; }

    [ForeignKey(nameof(Document))]
    public long? ParentTaskId { get; set; }
    public Task? ParentTask { get; set; }

    public List<Task>? SubTasks { get; set; }
}
