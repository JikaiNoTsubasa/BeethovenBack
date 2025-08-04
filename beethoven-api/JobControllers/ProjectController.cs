using System;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.ProjectModels;
using beethoven_api.JobManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[Authorize]
public class ProjectController(ProjectManager manager) : BeeController
{
    protected readonly ProjectManager _manager = manager;

    [HttpPost]
    [Route("api/projects")]
    public IActionResult CreateProject([FromBody] RequestCreateProject model)
    {
        var prj = _manager.CreateProject(model.Name, _loggedUserId, model.InitializePhases).ToDTO();
        return StatusCode(StatusCodes.Status201Created, prj);
    }

    [HttpGet]
    [Route("api/projects/{id}")]
    public IActionResult FetchProjectById([FromRoute] long id)
    {
        var prj = _manager.FetchProject(id).ToDTO();
        return StatusCode(StatusCodes.Status201Created, prj);
    }

    [HttpGet]
    [Route("api/projects/{id}/phases")]
    public IActionResult FetchProjectPhases([FromRoute] long id)
    {
        var phases = _manager.FetchProjectPhases(id).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status201Created, phases);
    }

    [HttpGet]
    [Route("api/my-projects")]
    public IActionResult FetchMyProjects()
    {
        var projects = _manager.FetchProjectsForUser(_loggedUserId).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, projects);
    }
}
