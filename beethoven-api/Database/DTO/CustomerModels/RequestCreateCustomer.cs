namespace beethoven_api.Database.DTO.CustomerModels;

public record RequestCreateCustomer
{
    public string? Name { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Comment { get; set; }
}
