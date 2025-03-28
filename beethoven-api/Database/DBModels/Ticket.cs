using System;

namespace beethoven_api.Database.DBModels;

public class Ticket : Entity
{
    public long? StatusId { get; set; }
    public TicketStatus? Status { get; set; }
    public string? Description { get; set; }

    public long? ProductId { get; set; }
    public Product? Product { get; set; }

    public long? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
    public long? ReviewedById { get; set; }
    public User? ReviewedBy { get; set; }
    public long? GitlabTicketId { get; set; }
    public List<TicketActivity>? Activities { get; set; }
    public long TypeId { get; set; }
    public TicketType? Type { get; set; }
    public long PriorityId { get; set; }
    public Priority? Priority { get; set; }
}
