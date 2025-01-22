namespace beethoven_api.Database.DTO.ProductModels;

public record RequestCreateProduct
{
    public string? Name { get; set; }
    public long? SLAId { get; set; }
    public long? CustomerId { get; set; }
    public string? CodeBaseLink { get; set; }
}
