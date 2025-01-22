using System;

namespace beethoven_api.Database.DBModels;

public class SLA : Entity
{
    public int ResponseTime { get; set; }
    public List<Product>? Products { get; set; }
}
