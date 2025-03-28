using beethoven_api.Database.DTO.TeamModels;

namespace beethoven_api.Database.DTO.UserModels;

public record ResponseUser : ResponseEntity{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public List<ResponseTeamSimplified>? Teams { get; set; }
}