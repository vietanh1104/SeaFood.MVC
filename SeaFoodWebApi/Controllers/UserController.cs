using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Application.Command.UserCommand;
using WebApp.Application.Query.UserQuery;

namespace SeaFoodWebApi.Controllers
{
    [ApiController]
    [Route("/api/user-profile")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPut("edit")]
        public async Task<IActionResult> EditProfile([FromBody] UserEditProfileCommand request)
        {
            try
            {
                var user = await _mediator.Send(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(detail: $"Bad request, message = {ex.Message}", statusCode: 400);
            }

        }
        [HttpPut("get/{id}")]
        public async Task<IActionResult> EditProfile(Guid id)
        {
            try
            {
                var user = await _mediator.Send(new UserGetByIdQuery
                {
                    id = id
                });
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(detail: $"Bad request, message = {ex.Message}", statusCode: 400);
            }

        }
    }
}
