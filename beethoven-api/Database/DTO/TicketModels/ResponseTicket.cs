using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.TicketModels;

public record ResponseTicket
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ResponseProduct? Product { get; set; }
    public ResponseUser? AssignedTo { get; set; }
    public ResponseUser? ReviewedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ResponseTicketStatus? Status { get; set; }
    public long? GitlabTicketId { get; set; }
    public List<ResponseTicketActivity>? Activities { get; set; }
}
