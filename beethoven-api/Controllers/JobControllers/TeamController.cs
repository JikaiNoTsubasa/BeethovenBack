using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.TeamModels;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

[Authorize]
public class TeamController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{

    [HttpPost]
    [Route("api/team")]
    public virtual IActionResult CreateTeam([FromForm] RequestCreateTeam model)
    {
        try
        {
            Team team = _engine.CreateTeam(model, _loggedUserId);
            return StatusCode(StatusCodes.Status200OK, team.ToDTO());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet]
    [Route("api/teams")]
    public virtual IActionResult FetchTeams()
    {
        try
        {
            List<ResponseTeam> teams = [.. _context.Teams.Include(t => t.Members).Select(t => t.ToDTO())];
            return StatusCode(StatusCodes.Status200OK, teams);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPut]
    [Route("api/team/addMember")]
    public virtual IActionResult AddTeamMember([FromForm] RequestAddTeamMember model)
    {
        try
        {
            Team team = _engine.AddMemberToTeam(model.TeamId, model.UserId);
            return StatusCode(StatusCodes.Status200OK, team.ToDTO());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
