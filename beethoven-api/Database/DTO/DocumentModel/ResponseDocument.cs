namespace beethoven_api.Database.DTO.DocumentModel;

public record class ResponseDocument : ResponseEntity
{

    public long? EntityId { get; set; }
    public string? EntityName { get; set; }
    public long VersionId { get; set; }
    public string Version { get; set; } = null!;
    public string Path { get; set; } = null!;
}
