using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Bases
{
    [ExcludeFromCodeCoverage]
    public abstract class ServiceFixtureBase
    {
        protected readonly INotifier _notifier;

        protected readonly IRepository _repository;

        public bool HasNotifications => _notifier.HasNotifications;

        public IEnumerable<Notify> Notifications => _notifier.GetNotifications();

        protected ServiceFixtureBase()
        {
            _notifier = new Notifier();
            _repository = RepositoryHelper.GetRepository();
        }

        protected ILogger<T> GetLogger<T>()
        {
            return new NullLogger<T>();
        }

        public void ClearNotifications() => _notifier.Clear();
    }
}
