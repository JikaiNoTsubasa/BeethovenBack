using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class ProjectPhase
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public Project Project { get; set; } = null!;

    [ForeignKey(nameof(PreviousPhase))]
    public long? PreviousPhaseId { get; set; }
    public ProjectPhase? PreviousPhase { get; set; }

    [ForeignKey(nameof(NextPhase))]
    public long? NextPhaseId { get; set; }
    public ProjectPhase? NextPhase { get; set; }
}
