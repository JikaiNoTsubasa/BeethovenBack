using System;

namespace beethoven_api.Database.DBModels;

public class Team : Entity
{

    public List<User>? Members { get; set; }
}
