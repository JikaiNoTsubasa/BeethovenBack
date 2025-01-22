using System;
using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DBModels;

public class TicketStatus
{
    [Key]
    public long Id { get; set; }
    public string? Name { get; set; }

    public List<Ticket>? Tickets { get; set; }
}
