using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class Project : Entity
{
    public List<ProjectPhase>? Phases { get; set; }

    [ForeignKey(nameof(CurrentPhase))]
    public long? CurrentPhaseId { get; set; }
    public ProjectPhase? CurrentPhase { get; set; }
    public List<ProjectPermission>? Permissions { get; set; }

    [ForeignKey(nameof(Customer))]
    public long? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [ForeignKey(nameof(Owner))]
    public long? OwnerId { get; set; }
    public User? Owner { get; set; }
}
