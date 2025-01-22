namespace beethoven_api.Database.DTO.TicketModels;

public record ResponseTicket
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
