using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Deviot.Hermes.ModbusTcp.Business.Bases
{
    public abstract class ServiceBase
    {
        protected readonly INotifier _notifier;
        protected readonly ILogger _logger;
        protected readonly IRepository _repository;

        protected const string INTERNAL_ERROR_MESSAGE = "Houve um problema ao realizar o processamento";

        protected ServiceBase(INotifier notifier, ILogger logger, IRepository repository)
        {
            _notifier = notifier;
            _repository = repository;
            _logger = logger;
        }

        protected virtual bool ValidateEntity<T>(IValidator<T> validator, T entity) where T : EntityBase
        {
            var result = validator.Validate(entity);
            if (result.IsValid)
                return true;

            foreach(var error in result.Errors)
                NotifyBadRequest(error.ErrorMessage);

            return false;
        }

        public virtual void NotifyOk(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.OK, message);
        }

        public virtual void NotifyCreated(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.Created, message);
        }

        public virtual void NotifyNoContent(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.NoContent, message);
        }

        public virtual void NotifyBadRequest(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.BadRequest, message);
        }

        public virtual void NotifyForbidden(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.Forbidden, message);
        }

        public virtual void NotifyNotFound(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.NotFound, message);
        }

        public virtual void NotifyInternalServerError(Exception exception)
        {
            var messages = Utils.GetAllExceptionMessages(exception);

            foreach(var message in messages)
                _logger.LogError(message);

            _notifier.Notify(HttpStatusCode.InternalServerError, INTERNAL_ERROR_MESSAGE);
        }
    }
}
