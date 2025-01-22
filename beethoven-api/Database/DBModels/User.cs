namespace beethoven_api.Database.DBModels;

public class User : Entity
{
    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Password { get; set; }
    public List<Ticket>? AssignedTickets { get; set; }
    public List<Ticket>? ReviewedTickets { get; set; }
}