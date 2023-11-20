using Productivity.Shared.Models.DTO.PostModels.AccountModels;

namespace Productivity.API.Services.Authentication.Base
{
    public interface IAuthService
    {
        public Task<string?> Login(AuthDTO record, CancellationToken cancellationToken);
        public Task<string?> GetJWT(string token, CancellationToken cancellationToken);
    }
}
