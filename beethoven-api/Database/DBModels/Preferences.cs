using System;
using System.ComponentModel.DataAnnotations;

namespace beethoven_api.Database.DBModels;

public class Preferences
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

}
