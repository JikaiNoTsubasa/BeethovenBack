namespace beethoven_api.Database.DTO.ProductModels;

public record ResponseProduct
{
    public long Id { get; set; }
    public string? Name { get; set; }
}
