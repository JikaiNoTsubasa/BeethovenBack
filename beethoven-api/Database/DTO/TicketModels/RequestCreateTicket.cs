namespace beethoven_api.Database.DTO.TicketModels;

public record RequestCreateTicket
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? ProductId { get; set; }
    public long? AssignedToId { get; set; }
    public long? ReviewedById { get; set; }
    public long? GitlabTicketId { get; set; }
    public long? StatusId { get; set; }
    public long PriorityId { get; set; }
    public long TypeId { get; set; }
}
