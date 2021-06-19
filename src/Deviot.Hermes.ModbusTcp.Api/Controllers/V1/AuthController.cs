using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Controllers.Base;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Api.Controllers.V1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(INotifier notifier, IAuthService authService) : base(notifier)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> LoginAsync()
        {
            var result = await _authService.LoginAsync(new Login("admin", "admin"));
            return CustomResponse(result);
        }
    }
}
