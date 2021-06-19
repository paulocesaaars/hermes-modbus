using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Settings;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Services
{
    public class AuthServiceTest
    {
        public AuthServiceTest()
        {

        }

        //[Fact(DisplayName = "Login válido")]
        //public async Task Valid_LoginAsync()
        //{
        //    IAuthService tokenService = new AuthService();

        //    var token = await tokenService.LoginAsync("admin", "admin");

        //    token.Id.Should().NotBeEmpty();
        //    token.AccessToken.Should().NotBeNullOrEmpty();
        //}

    }
}
