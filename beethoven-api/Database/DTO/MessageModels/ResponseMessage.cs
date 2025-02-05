using beethoven_api.Database.DTO.UserModels;

namespace beethoven_api.Database.DTO.MessageModels;

public record ResponseMessage
{
    public long Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public ResponseUser SourceUser { get; set; } = null!;

    public ResponseUser TargetUser { get; set; } = null!;

    public bool IsRead { get; set; }
}
