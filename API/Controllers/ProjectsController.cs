using Application.Projects.Commands;
using Application.Projects.DTOs;
using Application.Projects.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProjectsController : BaseApiController
{
    [HttpPost("create")]
    public async Task<ActionResult<string>> CreateProject(CreateProjectDto projectDto)
    {
        return HandleResult(await Mediator.Send(new CreateProjectCommand.Command { ProjectDto = projectDto }));
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<List<ListProjectDto>>> ListAllProjects()
    {
        return await Mediator.Send(new ListAllProjectsQuery.Query());
    }

    [HttpGet("{projectId}/users")]
    public async Task<ActionResult<List<UserInProjectDto>>> ListUsersInProject(string projectId)
    {
        return HandleResult(await Mediator.Send(new ListUsersInProjectQuery.Query { ProjectId = projectId }));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<ProjectDto>>> ListUserProjects(string userId)
    {
        return HandleResult(await Mediator.Send(new ListUserProjectsQuery.Query { UserId = userId }));
    }

    [HttpPost("add-user")]
    public async Task<ActionResult<string>> AddUserToProject(string userId, string projectId)
    {
        return HandleResult(await Mediator.Send(new AddUserToProjectCommand.Command { UserId = userId, ProjectId = projectId }));
    }

    [HttpPost("remove-user")]
    public async Task<ActionResult<string>> RemoveUserFromProject(string userId, string projectId)
    {
        return HandleResult(await Mediator.Send(new RemoveUserFromProjectCommand.Command { UserId = userId, ProjectId = projectId }));
    }
}