using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Bases;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Services
{
    public class AuthService : ServiceBase, IAuthService 
    {
        private readonly ITokenService _tokenService;

        private readonly IValidator<Login> _loginValidator;

        private const string NOTFOUND_USER_ERROR = "Usuário ou senha inválidos.";

        public AuthService(INotifier notifier, 
                           IRepository repository, 
                           ITokenService tokenService,
                           IValidator<Login> loginValidator
                          ) : base (notifier, repository)
        {
            _tokenService = tokenService;
            _loginValidator = loginValidator;
        }

        public async Task<Token> LoginAsync(Login login)
        {
            var result = ValidateEntity<Login>(_loginValidator, login);
            if(result)
            {
                var password = Utils.Encript(login.Password);
                var user = await _repository.Get<User>()
                                            .FirstOrDefaultAsync(x => x.UserName.ToLower() == login.UserName.ToLower()
                                                && x.Password == Utils.Encript(login.Password));

                if (user is not null)
                    return _tokenService.GenerateToken(user);

                _notifier.Notify(HttpStatusCode.NotFound, NOTFOUND_USER_ERROR);
            }

            return null;
        }
    }
}
