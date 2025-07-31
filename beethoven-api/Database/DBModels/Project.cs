using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class Project : Entity
{
    public List<ProjectPhase>? Phases { get; set; }
    
    [ForeignKey(nameof(CurrentPhase))]
    public long? CurrentPhaseId { get; set; }
    public ProjectPhase? CurrentPhase { get; set; }
}
