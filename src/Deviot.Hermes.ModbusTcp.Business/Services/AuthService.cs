using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Bases;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly ITokenService _tokenService;

        private readonly IValidator<Login> _loginValidator;

        private UserInfo _userLogged;

        private const string NOTFOUND_USER_ERROR = "Usuário ou senha inválidos.";

        public bool IsAuthenticated { get; private set; }

        public AuthService(INotifier notifier, 
                           ILogger<AuthService> logger,
                           IRepository repository, 
                           ITokenService tokenService,
                           IValidator<Login> loginValidator
                          ) : base (notifier, logger, repository)
        {
            _tokenService = tokenService;
            _loginValidator = loginValidator;

            IsAuthenticated = false;
        }

        public async Task<Token> LoginAsync(Login login)
        {
            try
            {
                var result = ValidateEntity<Login>(_loginValidator, login);
                if (result)
                {
                    var password = Utils.Encript(login.Password);
                    var user = await _repository.Get<User>()
                                                .FirstOrDefaultAsync(x => x.Enabled
                                                    && x.UserName.ToLower() == login.UserName.ToLower()
                                                    && x.Password == Utils.Encript(login.Password));

                    if (user is not null)
                        return _tokenService.GenerateToken(user);

                    _notifier.Notify(HttpStatusCode.NotFound, NOTFOUND_USER_ERROR);
                }

                return null;
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
                return null;
            }
        }

        public UserInfo GetLoggedUser() => _userLogged;

        public void SetLoggedUser(UserInfo user)
        {
            if(IsAuthenticated is false)
            {
                IsAuthenticated = true;
                _userLogged = user;
            }
        }
    }
}
