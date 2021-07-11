using AutoMapper;
using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Bases
{
    [ExcludeFromCodeCoverage]
    public abstract class ControllerTestBase
    {
        protected readonly INotifier _notifier;

        protected readonly IMapper _mapper;

        protected readonly AutoMocker _mocker;

        protected const string INTERNAL_ERROR_MESSAGE = "A requisição não foi executada com sucesso, erro não identificado";

        protected ControllerTestBase()
        {
            _notifier = new Notifier();

            var mappingConfig = new MapperConfiguration(options =>
            {
                options.AddProfile(new EntityToViewModelMapping());
                options.AddProfile(new ViewModelToEntityMapping());
            });
            _mapper = mappingConfig.CreateMapper();

            _mocker = new AutoMocker();
            _mocker.Use<INotifier>(_notifier);
            _mocker.Use<IMapper>(_mapper);
        }

        protected GenericActionResult<T> GetGenericActionResult<T>(ActionResult<T> actionResult)
        {
            var contentResult = actionResult.Result as ContentResult;
            return Utils.Deserializer<GenericActionResult<T>>(contentResult.Content);
        }

        protected GenericActionResult<object> GetGenericActionResult(ActionResult actionResult)
        {
            var contentResult = actionResult as ContentResult;
            return Utils.Deserializer<GenericActionResult<object>>(contentResult.Content);
        }

        protected int? GetHttpStatusCode<T>(ActionResult<T> actionResult)
        {
            return (actionResult.Result as ContentResult).StatusCode;
        }

        protected int? GetHttpStatusCode(ActionResult actionResult)
        {
            return (actionResult as ContentResult).StatusCode;
        }
    }
}
