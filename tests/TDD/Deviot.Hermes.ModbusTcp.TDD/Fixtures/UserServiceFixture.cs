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
            var paulo = UserFake.GetUserPaulo(true);
            var bruna = UserFake.GetUserBruna(true);
            var paula = UserFake.GetUserPaula(true);

            await _repository.AddAsync<User>(paulo);
            await _repository.AddAsync<User>(bruna);
            await _repository.AddAsync<User>(paula);
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

        public UserInfo GetUserInfoAdmin() => UserInfoFake.GetUserAdmin();

        public User GetUserAdmin(bool encriptPassword = false) => UserFake.GetUserAdmin(encriptPassword);


        public UserInfo GetUserInfoPaulo() => UserInfoFake.GetUserPaulo();

        public User GetUserPaulo(bool encriptPassword = false) => UserFake.GetUserPaulo(encriptPassword);

        public UserInfo GetUserInfoBruna() => UserInfoFake.GetUserBruna();

        public UserInfo GetUserInfoPaula() => UserInfoFake.GetUserPaula();

        public UserService GetServiceWithLoggedAdmin()
        {
            var user = GetUserInfoAdmin();
            return GetService(user);
        }


        public UserService GetService(UserInfo loggedUser)
        {
            var authService = GetAuthService();
            authService.SetLoggedUser(loggedUser.Clone() as UserInfo);
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
