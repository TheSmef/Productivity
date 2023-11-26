using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Authentication.Base;
using Productivity.Shared.Models.DTO.PostModels.AccountModels;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Utility.TokenHelpers;
using System.Linq.Expressions;

namespace Productivity.API.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAccountRepository accountRepository, 
            ITokenRepository tokenRepository,
            IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _configuration = configuration;
        }
        public async Task<string?> GetJWT(string token, CancellationToken cancellationToken)
        {
            var refresh = await _tokenRepository.GetItem(token, cancellationToken, 
                new() { x => x.Account });
            if (refresh == null)
            {
                return null;
            }
            return TokenHandler.CreateJwtToken(refresh.Account, _configuration);
        }

        public async Task<string?> Login(AuthDTO record, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.AuthUser(record, cancellationToken);
            if (account == null)
            {
                return null;
            }
            var token = new Token()
            {
                Account = account,
                TokenStr = TokenHandler.GenerateRefreshToken()
            };
            await _tokenRepository.AddItem(token, cancellationToken);
            return token.TokenStr;
        }
    }
}
