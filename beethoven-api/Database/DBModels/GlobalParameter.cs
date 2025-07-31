using System;

namespace beethoven_api.Database.DBModels;

public class GlobalParameter
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? Description { get; set; }
}
