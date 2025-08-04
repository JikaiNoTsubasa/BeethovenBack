using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.Database.DBModels;

public class ProjectPermission
{
    [Key]
    public long Id { get; set; }

    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey(nameof(Project))]
    public long ProjectId { get; set; }
    public Project? Project { get; set; }

    public bool CanRead { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanCreateIssues { get; set; }

    [NonAction]
    public void SetAllPermissions(bool value) => (CanRead, CanUpdate, CanCreateIssues) = (value, value, value);
}
