using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DBModels;

public class Entity{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedById { get; set; }
    public DateTime? DeletedAt { get; set; }
    public long? DeletedById { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedById { get; set; }

    public void MarkCreated(long userId) {
        CreatedAt = DateTime.UtcNow;
        CreatedById = userId;
    }

    public void MarkUpdated(long userId) {
        UpdatedAt = DateTime.UtcNow;
        UpdatedById = userId;
    }

    public void MarkDeleted(long userId) {
        DeletedAt = DateTime.UtcNow;
        DeletedById = userId;
    }
}