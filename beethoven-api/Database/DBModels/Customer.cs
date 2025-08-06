using System;

namespace beethoven_api.Database.DBModels;

public class Customer : Entity
{
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Comment { get; set; }
    public List<User>? Members { get; set; }
    public List<Project>? Projects { get; set; }
}
