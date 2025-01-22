using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Database.DTO.TicketModels;

namespace beethoven_api.Database.DTO.CustomerModels;

public record ResponseCustomer
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Comment { get; set; }
    public List<ResponseTicket>? Tickets { get; set; }
    public List<ResponseProduct>? Products { get; set; }
}
