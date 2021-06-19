using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Deviot.Hermes.ModbusTcp.Api.Controllers.Base
{
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        protected INotifier _notifier;

        protected CustomControllerBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        private static ContentResult GenerateContentResult(HttpStatusCode httpStatusCode, string message, object data)
        {
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var customActionResult = new CustomActionResult(message, data);
            var content = JsonSerializer.Serialize(customActionResult, jsonSerializerOptions);

            return new ContentResult { StatusCode = (int)httpStatusCode, 
                                       Content = content, 
                                       ContentType = $"application/json" 
                                     };
        }

        protected ActionResult CustomResponse(object value = null)
        {
            if(_notifier.HasNotifications)
            {
                var notifies = _notifier.GetNotifications();
                var notify = notifies.First();
                return GenerateContentResult(notify.Type, notify.Message, value);
            }

            return Ok(new CustomActionResult("A requisição foi executada com sucesso.", value));
        }
    }
}
