using System;

namespace beethoven_api.Database.DBModels;

public class Product : Entity
{
    public long? SLAId { get; set; }
    public SLA? SLA { get; set; }

    public long? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string? CodeBaseLink { get; set; }
}
