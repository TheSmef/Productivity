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
        private readonly IAccountRepository accountRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IConfiguration configuration;
        public AuthService(IAccountRepository accountRepository, 
            ITokenRepository tokenRepository,
            IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.tokenRepository = tokenRepository;
            this.configuration = configuration;
        }
        public async Task<string?> GetJWT(string token, CancellationToken cancellationToken)
        {
            var refresh = await tokenRepository.GetItem(token, cancellationToken, 
                new() { x => x.Account });
            if (refresh == null)
            {
                return null;
            }
            return TokenHandler.CreateJwtToken(refresh.Account, configuration);
        }

        public async Task<string?> Login(AuthDTO record, CancellationToken cancellationToken)
        {
            var account = await accountRepository.AuthUser(record, cancellationToken);
            if (account == null)
            {
                return null;
            }
            var token = new Token()
            {
                Account = account,
                TokenStr = TokenHandler.GenerateRefreshToken()
            };
            await tokenRepository.AddItem(token, cancellationToken);
            return token.TokenStr;
        }
    }
}
