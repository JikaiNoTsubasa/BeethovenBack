namespace beethoven_api.Database.DTO.TeamModels;

public record RequestAddTeamMember
{
    public long TeamId { get; set; }
    public long UserId { get; set; }
}
