using beethoven_api.Database.DTO.ProductModels;

namespace beethoven_api.Database.DTO.SLAModels;

public record ResponseSLA
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<ResponseProduct>? Products { get; set; }
}
