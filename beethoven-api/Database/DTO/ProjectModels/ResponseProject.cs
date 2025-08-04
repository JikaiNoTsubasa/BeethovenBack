namespace beethoven_api.Database.DTO.ProjectModels;

public record class ResponseProject : ResponseEntity
{
    public long? OwnerId { get; set; }
    public string? OwnerName { get; set; }
}
