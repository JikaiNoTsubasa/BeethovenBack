namespace beethoven_api.Database.DBModels;

public class User : Entity
{
    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Password { get; set; }
    public DateTime? Lastconnection { get; set; }
    public bool CanLogin { get; set; }
    public string? Avatar { get; set; }
    public long PreferencesId { get; set; }
    public Preferences? Preferences { get; set; }
    public List<Team>? Teams { get; set; }
    public List<ProjectPermission>? Permissions { get; set; }
    public List<Project>? Projects { get; set; }
    public List<DocumentVersion>? CheckedOutDocuments { get; set; }
}