using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthWebApi.Models.DTOs;
using MongoAuthWebApi.Queries;

namespace MongoAuthWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IMediator _mediator;

	public UsersController(IMediator mediator)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	[HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
