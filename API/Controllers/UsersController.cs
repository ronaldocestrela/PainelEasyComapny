using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserAccessor userAccessor) : BaseApiController
{
    private readonly IUserAccessor _userAccessor = userAccessor;

    [HttpGet("list-all")]
    public async Task<ActionResult<List<UserDto>>> ListAll()
    {
        var result = await Mediator.Send(new ListAllUsersQuery.Query());
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery.Query { Id = id });
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateUserCommand.Command command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Update(string id, UpdateUserCommand.Command command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteUserCommand.Command { Id = id });
        return HandleResult(result);
    }

    [HttpGet("current")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userInfo = await _userAccessor.GetUserAsync();
        var role = _userAccessor.GetUserRoleNameByUserIdAsync(userInfo.Id);
        var userDto = new UserDto
        {
            Id = userInfo.Id,
            Name = $"{userInfo.FirstName} {userInfo.LastName}",
            Email = userInfo.Email,
            Role = role,
            IsActive = true // Assuming active, or check if needed
        };
        return Ok(userDto);
    }
}