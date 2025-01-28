using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.TicketModels;

public record ResponseTicketActivity
{
    public long Id { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public ResponseUser? User { get; set; }
}
