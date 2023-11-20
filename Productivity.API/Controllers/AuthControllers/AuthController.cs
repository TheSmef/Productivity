using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Authentication.Base;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;

namespace Productivity.API.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> SignIn(AuthDTO model, 
            CancellationToken cancellationToken)
        {
            var result = await _authService.Login(model, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<string>> GetJWT([FromBody] string token, 
            CancellationToken cancellationToken)
        {
            var result = await _authService.GetJWT(token, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
