using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.TDD.Bases;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Fixtures
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
            var paulo = GetUserPaulo(true);
            var bruna = GetUserBruna(true);
            var paula = GetUserPaula(true);

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

        public User GetUserAdmin(bool passwordEncript = false) => UserFake.GetUserAdmin(passwordEncript);

        public User GetUserPaulo(bool passwordEncript = false) => UserFake.GetUserPaulo(passwordEncript);

        public User GetUserBruna(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("630994d9e6c34d5cb823569560697d67"),
                                     "Bruna Stefano Marques",
                                     "bruna",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public User GetUserPaula(bool passwordEncript = false)
        {
            var password = "123456";
            return new User(new Guid("f22e81455a6f4961922a516c54d33dba"),
                                     "Paula Stefano Souza",
                                     "paula",
                                     passwordEncript ? Utils.Encript(password) : password,
                                     true,
                                     false);
        }

        public UserInfo GetUserInfoAdmin() => UserFake.GetUserAdmin();

        public UserInfo GetUserInfoPaulo() => UserFake.GetUserPaulo();

        public UserInfo GetUserInfoBruna()
        {
            return new UserInfo(new Guid("630994d9e6c34d5cb823569560697d67"),
                                     "Bruna Stefano Marques",
                                     "bruna",
                                     true,
                                     false);
        }

        public UserInfo GetUserInfoPaula()
        {
            return new UserInfo(new Guid("f22e81455a6f4961922a516c54d33dba"),
                                     "Paula Stefano Souza",
                                     "paula",
                                     true,
                                     false);
        }

        public IEnumerable<UserInfo> GetUserInfoAll()
        {
            var users = new List<UserInfo>(4);
            users.Add(GetUserInfoAdmin());
            users.Add(GetUserInfoPaulo());
            users.Add(GetUserInfoBruna());
            users.Add(GetUserInfoPaula());

            return users;
        }

        public IEnumerable<User> GetUserAll(bool passwordEncript = false)
        {
            var users = new List<User>(4);
            users.Add(GetUserAdmin(passwordEncript));
            users.Add(GetUserPaulo(passwordEncript));
            users.Add(GetUserBruna(passwordEncript));
            users.Add(GetUserPaula(passwordEncript));

            return users;
        }

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
