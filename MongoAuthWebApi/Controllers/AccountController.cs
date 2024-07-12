using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthWebApi.Commands;
using MongoAuthWebApi.Models;
using MongoAuthWebApi.Models.DTOs;
using MongoAuthWebApi.Primitives;

namespace MongoAuthWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;

	public AccountController(IMediator mediator)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var loginCommand = new LoginCommand(loginModel.Email, loginModel.Password);
        var result = await _mediator.Send(loginCommand);

        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(result.Error);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var registerCommand = new RegisterCommand(registerModel.Email, registerModel.Password);

        var result = await _mediator.Send(registerCommand);

        if (result.IsSuccess) return CreatedAtAction(nameof(this.Register), result.Value);
        return BadRequest(result.Error);
    }
}