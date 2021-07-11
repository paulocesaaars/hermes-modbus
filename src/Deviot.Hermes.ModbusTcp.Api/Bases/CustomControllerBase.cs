using AutoMapper;
using Deviot.Common;
using Deviot.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Deviot.Hermes.ModbusTcp.Api.Bases
{
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public abstract class CustomControllerBase : ControllerBase
    {
        protected readonly INotifier _notifier;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        private const string CONTENT_TYPE = "application/json";
        private const string OK_MESSAGE = "A requisição foi executada com sucesso";
        private const string INTERNAL_ERROR_MESSAGE = "A requisição não foi executada com sucesso, erro não identificado";

        protected CustomControllerBase(INotifier notifier, 
                                       IMapper mapper, 
                                       ILogger logger)
        {
            _notifier = notifier;
            _mapper = mapper;
            _logger = logger;
        }

        private static string Serialize(CustomActionResult customActionResult)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Serialize(customActionResult, options);
        }

        private static ContentResult GenerateContentResult(HttpStatusCode httpStatusCode, 
                                                           IEnumerable<string> messages, 
                                                           object data) 
        {
            var json = Serialize(new CustomActionResult(messages, data));

            return new ContentResult { StatusCode = (int) httpStatusCode, 
                                       Content = json, 
                                       ContentType = CONTENT_TYPE
                                     };
        }

        protected ActionResult CustomResponse(object value = null)
        {
            var messages = new List<string>(10);
            var httpStatusCode = HttpStatusCode.OK;
            
            if (_notifier.HasNotifications)
            {
                var notifies = _notifier.GetNotifications();

                if (notifies.Any(x => x.Type == HttpStatusCode.Unauthorized))
                {
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.Unauthorized).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.InternalServerError))
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.InternalServerError).Message);
                }
                else if(notifies.Any(x => x.Type == HttpStatusCode.Forbidden))
                {
                    httpStatusCode = HttpStatusCode.Forbidden;
                    foreach (var notify in notifies.Where(x => x.Type == HttpStatusCode.Forbidden))
                        messages.Add(notify.Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.NotFound))
                {
                    httpStatusCode = HttpStatusCode.NotFound;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.NotFound).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.NoContent))
                {
                    httpStatusCode = HttpStatusCode.NoContent;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.NoContent).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.Created))
                {
                    httpStatusCode = HttpStatusCode.Created;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.Created).Message);
                }
                else if (notifies.Any(x => x.Type == HttpStatusCode.OK))
                {
                    httpStatusCode = HttpStatusCode.OK;
                    messages.Add(notifies.First(x => x.Type == HttpStatusCode.OK).Message);
                }
                else
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    messages.Add(INTERNAL_ERROR_MESSAGE);
                }

                return GenerateContentResult(httpStatusCode, messages, value);
            }

            messages.Add(OK_MESSAGE);
            return GenerateContentResult(httpStatusCode, messages, value);
        }

        protected ActionResult ReturnActionResultForGenericError(Exception exception)
        {
            var errors = Utils.GetAllExceptionMessages(exception);
            foreach (var error in errors)
                _logger.LogError(error);

            return GenerateContentResult(HttpStatusCode.InternalServerError, new List<string>(1) { INTERNAL_ERROR_MESSAGE }, null);
        }
    }
}
