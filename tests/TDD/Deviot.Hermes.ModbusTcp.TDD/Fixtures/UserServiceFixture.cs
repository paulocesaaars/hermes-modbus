using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Fixtures.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures.Business.Services
{
    [ExcludeFromCodeCoverage]
    [CollectionDefinition(nameof(UserServiceCollection))]
    public class UserServiceCollection : ICollectionFixture<UserServiceFixture>
    {
    }

    public class UserServiceFixture : ServiceFixtureBase, IDisposable
    {
        public UserServiceFixture()
        {
            var task = PopulateDatabaseAsync();
            task.Wait();
        }

        private async Task PopulateDatabaseAsync()
        {
            var user = UserFake.GetUserNormal();
            await _repository.AddAsync<User>(user);
        }

        private AuthService GetAuthService()
        {
            var loginValidator = new LoginValidator();
            var tokenService = TokenHelper.GetTokenService();
            return new AuthService(_notifier, 
                                   GetLogger<AuthService>(), 
                                   _repository, 
                                   tokenService, 
                                   loginValidator);
        }

        public UserInfo GetUserAdmin() => UserInfoFake.GetUserAdmin();

        public UserInfo GetUserNormal() => UserInfoFake.GetUserNormal();

        public UserService GetServiceWithLoggedAdmin()
        {
            var user = GetUserAdmin();
            return GetService(user);
        }


        public UserService GetService(UserInfo loggedUser)
        {
            var authService = GetAuthService();
            authService.SetLoggedUser(loggedUser);
            var userValidator = new UserValidator();
            var userInfoValidator = new UserInfoValidator();
            var userPasswordValidator = new UserPasswordValidator();
            return new UserService(_notifier, 
                                   GetLogger<UserService>(), 
                                   _repository, 
                                   authService, 
                                   userValidator, 
                                   userInfoValidator, 
                                   userPasswordValidator);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
