using System;
using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DBModels;

public class Message
{
    [Key]
    public long Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public long SourceUserId { get; set; }
    public User SourceUser { get; set; } = null!;

    public long TargetUserId { get; set; }
    public User TargetUser { get; set; } = null!;

    public bool IsRead { get; set; }
}
