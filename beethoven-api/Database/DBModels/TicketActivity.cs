using System;
using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DBModels;

public class TicketActivity
{
    [Key]
    public long Id { get; set; }
    public long TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public long UserId { get; set; }
    public User? User { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
