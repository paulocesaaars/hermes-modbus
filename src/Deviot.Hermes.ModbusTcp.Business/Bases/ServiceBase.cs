using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using FluentValidation;
using System.Net;

namespace Deviot.Hermes.ModbusTcp.Business.Bases
{
    public abstract class ServiceBase
    {
        protected readonly INotifier _notifier;

        protected readonly IRepository _repository;

        protected ServiceBase(INotifier notifier, IRepository repository)
        {
            _notifier = notifier;
            _repository = repository;
        }

        protected virtual bool ValidateEntity<T>(IValidator<T> validator, T entity) where T : EntityBase
        {
            var result = validator.Validate(entity);
            if (result.IsValid)
                return true;

            foreach(var error in result.Errors)
                _notifier.Notify(HttpStatusCode.Forbidden, error.ErrorMessage);

            return false;
        }
    }
}
