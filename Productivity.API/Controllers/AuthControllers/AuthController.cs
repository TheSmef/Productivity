using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Productivity.API.Services.Authentication.Base;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;

namespace Productivity.API.Controllers.AuthControllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<string>> SignIn(AuthDTO model, 
            CancellationToken cancellationToken)
        {
            var result = await _service.Login(model, cancellationToken);
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
            var result = await _service.GetJWT(token, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> SignOut(string token,
            CancellationToken cancellationToken)
        {
            await _service.SignOut(token, cancellationToken);
            return Ok();
        }
    }
}
