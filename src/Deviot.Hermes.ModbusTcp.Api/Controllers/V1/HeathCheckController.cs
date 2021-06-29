using AutoMapper;
using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Deviot.Hermes.ModbusTcp.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/heath-check")]
    public class HeathCheckController : CustomControllerBase
    {

        public HeathCheckController(INotifier notifier, 
                                    IMapper mapper, 
                                    ILogger<HeathCheckController> logger
                                   ) : base(notifier, mapper, logger)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get() => Ok();
    }
}
