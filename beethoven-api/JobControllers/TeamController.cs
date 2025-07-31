using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.TeamModels;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.JobControllers;

[Authorize]
public class TeamController : BeeController
{

    [HttpPost]
    [Route("api/team")]
    public virtual IActionResult CreateTeam([FromForm] RequestCreateTeam model)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("api/teams")]
    public virtual IActionResult FetchTeams()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("api/team/addMember")]
    public virtual IActionResult AddTeamMember([FromForm] RequestAddTeamMember model)
    {
        throw new NotImplementedException();
    }
}
