using Deviot.Hermes.ModbusTcp.Api.Controllers.V1;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Api.Controllers.V1
{
    public class HeathCheckControllerTest : ControllerTestBase
    {
        [Fact]
        public void Get_ReturnOk()
        {
            var controller = _mocker.CreateInstance<HeathCheckController>();

            var result = controller.Get();

            result.Should().BeOfType<OkResult>();
        }
    }
}
