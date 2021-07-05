using AutoMapper;
using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Bases
{
    [ExcludeFromCodeCoverage]
    public abstract class ControllerTestBase
    {
        protected AutoMocker Mocker { get; set; }

        protected ControllerTestBase()
        {
            var mappingConfig = new MapperConfiguration(options =>
            {
                options.AddProfile(new EntityToModelViewMapping());
                options.AddProfile(new ModelViewToEntityMapping());
            });
            var mapper = mappingConfig.CreateMapper();

            Mocker = new AutoMocker();
            Mocker.Use<INotifier>(new Notifier());
            Mocker.Use<IMapper>(mapper);
        }

        protected ILogger<T> GetLogger<T>()
        {
            return new NullLogger<T>();
        }

        //protected static T GetValueResponse<T>(ContentResult result)
        //{

        //}
    }
}
