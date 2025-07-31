namespace beethoven_api.Database.DTO.ProjectModels;

public record class ResponseProjectPhase
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long ProjectId { get; set; }
    public long? PreviousPhaseId { get; set; }
    public long? NextPhaseId { get; set; }

}
