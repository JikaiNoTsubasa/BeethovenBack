using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace beethoven_api.Database.DBModels;

public class InboxMessage : Entity
{
    [ForeignKey(nameof(From))]
    public long FromId { get; set; }
    public User From { get; set; } = null!;

    [ForeignKey(nameof(To))]
    public long ToId { get; set; }
    public User To { get; set; } = null!;

    public DateTime DateSent { get; set; } = DateTime.UtcNow;
    public string? Message { get; set; }
}
