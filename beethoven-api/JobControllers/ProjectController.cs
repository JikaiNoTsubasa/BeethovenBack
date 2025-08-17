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

    #region Projects

    [HttpPost]
    [Route("api/projects")]
    public IActionResult CreateProject([FromBody] RequestCreateProject model)
    {
        long ownerId = _loggedUserId;
        if (model.OwnerId.HasValue) ownerId = model.OwnerId.Value;
        var prj = _manager.CreateProject(model.Name, ownerId, _loggedUserId, model.InitializePhases, model.CustomerId).ToDTO();
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

    [HttpGet]
    [Route("api/my-projects/{id}")]
    public IActionResult FetchMyProjects([FromRoute] long id)
    {
        var project = _manager.FetchProjectForUser(id, _loggedUserId)?.ToDTO();
        return StatusCode(StatusCodes.Status200OK, project);
    }

    [HttpGet]
    [Route("api/my-projects/{id}/permissions")]
    public IActionResult FetchMyProjectPermissions([FromRoute] long id)
    {
        var permissions = _manager.FetchProjectPermissions(id, _loggedUserId).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, permissions);
    }

    [HttpGet]
    [Route("api/projects/{id}/permissions")]
    public IActionResult FetchProjectPermissions([FromRoute] long id)
    {
        var permissions = _manager.FetchProjectPermissions(id).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, permissions);
    }

    [HttpGet]
    [Route("api/projects/{id}/documents")]
    public IActionResult FetchProjectDocuments([FromRoute] long id)
    {
        var documents = _manager.FetchProjectDocuments(id).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, documents);
    }

    #endregion

    #region Tasks

    [HttpPost]
    [Route("api/projects/{id}/tasks")]
    public IActionResult CreateProjectTask([FromRoute] long id, [FromBody] RequestCreateTask model)
    {
        var task = _manager.CreateTaskForProject(
            id,
            model.Name,
            model.PhaseId,
            _loggedUserId,
            model.Description,
            model.StartDate,
            model.EndDate,
            model.AssigneeIds,
            model.EstimatedMinutes,
            model.Priority
        ).ToDTO();
        return StatusCode(StatusCodes.Status200OK, task);
    }

    [HttpGet]
    [Route("api/projects/{id}/tasks")]
    public IActionResult FetchProjectTasks([FromRoute] long id)
    {
        var tasks = _manager.FetchProjectTasks(id).Select(p => p.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, tasks);
    }

    #endregion
}
