using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Database.DTO.SLAModels;

namespace beethoven_api.Database.DTO.ProductModels;

public record ResponseProduct
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public ResponseCustomerSimplified? Customer { get; set; }
    public ResponseSLASimplified? SLA { get; set; }
}
