using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DTO.ProjectModels;

public record class RequestCreateProject
{
    [Required]
    public string Name { get; set; } = null!;
    public bool? InitializePhases { get; set; }
}
