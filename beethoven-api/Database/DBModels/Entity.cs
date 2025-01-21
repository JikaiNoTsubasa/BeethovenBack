namespace beethoven_api.Database.DBModels;

public class Entity{
    public long Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedById { get; set; }
    public DateTime DeletedAt { get; set; }
    public long DeletedById { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UpdatedById { get; set; }
}