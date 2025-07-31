using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class Document:Entity
{
    [ForeignKey(nameof(Entity))]
    public long? EntityId { get; set; }
    public Entity? Entity { get; set; }
    public List<DocumentVersion> Versions { get; set; } = [];

    [NotMapped]
    public DocumentVersion CurrentVersion { get
        {
            return Versions.Where(v => v.IsCurrent).FirstOrDefault() ?? throw new Exception($"Could not find current version for document {Id}");
        }
    }
}
