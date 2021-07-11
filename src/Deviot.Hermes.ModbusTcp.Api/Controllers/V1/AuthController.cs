using AutoMapper;
using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Bases;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(INotifier notifier, 
                              IMapper mapper, 
                              ILogger<AuthController> logger, 
                              IAuthService authService
                             ) : base(notifier, mapper, logger)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<TokenViewModel>> LoginAsync(LoginViewModel loginModelView)
        {
            try
            {
                var login = _mapper.Map<Login>(loginModelView);
                var tokenViewModel = _mapper.Map<TokenViewModel>(await _authService.LoginAsync(login));
                return CustomResponse(tokenViewModel);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
