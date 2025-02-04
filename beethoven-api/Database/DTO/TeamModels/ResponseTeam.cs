
using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.TeamModels;

public record ResponseTeam : ResponseEntity
{
    public List<ResponseUser>? Members { get; set; }
}
