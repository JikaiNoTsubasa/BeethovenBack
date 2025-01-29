namespace beethoven_api.Database.DTO.TicketModels;

public record ResponsePriority
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
