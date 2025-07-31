using System;

namespace beethoven_api.Database.DBModels;

public class Project : Entity
{
    public List<ProjectPhase>? Phases { get; set; }
    public long? CurrentPhaseId { get; set; }
    public ProjectPhase? CurrentPhase { get; set; }
}
