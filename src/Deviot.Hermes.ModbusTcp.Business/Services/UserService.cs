using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Bases;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IAuthService _authService;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<UserInfo> _userInfoValidator;
        private readonly IValidator<UserPassword> _userPasswordValidator;

        private const string USER_CREATED = "O usuário foi criado com sucesso";
        private const string USER_UPDATED = "O usuário foi atualizado com sucesso";
        private const string USER_DELETED = "O usuário foi deletado com sucesso";
        private const string USER_NOT_FOUND = "O usuário não foi encontrado";
        private const string INVALID_PASSWORD = "Senha atual inválida";
        private const string USERNAME_ALREADY_EXISTS = "O nome de usuário informado já existe";
        private const string CHANGE_ANOTHER_USER_DATA = "Não é permitido alterar dados de outro usuário";
        private const string CHANGE_USER_TO_ADMINISTRATOR = "Somente um administrador pode criar ou alterar um usuário administrador";
        private const string INSERT_OR_DELETE_USER_AUTHORIZATION = "Somente um administrador pode criar ou deletar um usuário";
        private const string DELETE_ADMINISTRATOR_LIMITS = "Não é possivel deletar todos os usuários administradores";

        public UserService(INotifier notifier, 
                          ILogger<UserService> logger, 
                          IRepository repository, 
                          IAuthService authService, 
                          IValidator<User> userValidator, 
                          IValidator<UserInfo> userInfoValidator,
                          IValidator<UserPassword> userPasswordValidator
                         ) : base(notifier, logger, repository)
        {
            _authService = authService;
            _userValidator = userValidator;
            _userInfoValidator = userInfoValidator;
            _userPasswordValidator = userPasswordValidator;
        }

        private bool CheckInsertOrDeleteAuthorization()
        {
            var loggedUser = _authService.GetLoggedUser();
            if (loggedUser.Administrator)
                return true;

            NotifyUnauthorized(INSERT_OR_DELETE_USER_AUTHORIZATION);
            return false;
        }

        private bool CheckUpdateUserAuthorization(UserInfo user)
        {
            var loggedUser = _authService.GetLoggedUser();
            if (loggedUser.Administrator)
                return true;

            if (user.Id == loggedUser.Id)
            {
                if(user.Administrator)
                {
                    NotifyUnauthorized(CHANGE_USER_TO_ADMINISTRATOR);
                    return false;
                }

                return true;
            }

            NotifyUnauthorized(CHANGE_ANOTHER_USER_DATA);
            return false;
        }

        private bool CheckUpdatePasswordAuthorization(Guid userId)
        {
            var loggedUser = _authService.GetLoggedUser();
            if (loggedUser.Id == userId)
                return true;

            NotifyUnauthorized(CHANGE_ANOTHER_USER_DATA);
            return false;
        }

        private async Task<bool> CheckUserNameExistAsync(UserInfo user)
        {
            try
            {
                var result = await _repository.Get<User>()
                                              .AnyAsync(x => x.UserName.ToLower() == user.UserName.ToLower()
                                                        && x.Id != user.Id);

                return result;
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
                return false;
            }
        }


        public async Task<UserInfo> GetAsync(Guid id)
        {
            try
            {
                var user = await _repository.Get<User>()
                                            .FirstOrDefaultAsync(x => x.Id == id);

                if (user is null)
                    NotifyNotFound(USER_NOT_FOUND);

                return user;
            }
            catch(Exception exception)
            {
                NotifyInternalServerError(exception);
                return null;
            }
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            try
            {
                var users = await _repository.Get<User>()
                                             .ToListAsync();

                return users;
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
                return null;
            }
        }

        public async Task InsertAsync(User user)
        {
            try
            {
                var valid = ValidateEntity<User>(_userValidator, user);
                if (valid)
                {
                    if (CheckInsertOrDeleteAuthorization())
                    {
                        var check = await CheckUserNameExistAsync(user.UserName);

                        if (check)
                        {
                            NotifyForbidden(USERNAME_ALREADY_EXISTS);
                        }
                        else
                        {
                            user.SetPassword(Utils.Encript(user.Password));
                            await _repository.AddAsync<User>(user);
                            NotifyCreated(USER_CREATED);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
            }
        }

        public async Task UpdateAsync(UserInfo userInfo)
        {
            try
            {
                var valid = ValidateEntity<UserInfo>(_userInfoValidator, userInfo);
                if (valid)
                {
                    if(CheckUpdateUserAuthorization(userInfo))
                    {
                        var user = await _repository.Get<User>()
                                                    .FirstOrDefaultAsync(x => x.Id == userInfo.Id);

                        if (user is not null)
                        {
                            var check = await CheckUserNameExistAsync(userInfo);
                            if (check)
                            {
                                NotifyForbidden(USERNAME_ALREADY_EXISTS);
                            }
                            else
                            {
                                user.SetFullName(userInfo.FullName);
                                user.SetUserName(userInfo.UserName);
                                user.SetEnabled(userInfo.Enabled);
                                user.SetAdministrator(userInfo.Administrator);

                                await _repository.EditAsync<User>(user);
                                NotifyOk(USER_UPDATED);
                            }
                        }
                        else
                        {
                            NotifyNotFound(USER_NOT_FOUND);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (CheckInsertOrDeleteAuthorization())
            {
                var user = await _repository.Get<User>()
                                        .FirstOrDefaultAsync(x => x.Id == id);

                if (user is null)
                {
                    NotifyNotFound(USER_NOT_FOUND);
                    return;
                }

                if (user.Administrator)
                {
                    var count = await _repository.Get<User>().CountAsync(x => x.Administrator);
                    if (count == 1)
                    {
                        NotifyUnauthorized(DELETE_ADMINISTRATOR_LIMITS);
                        return;
                    }
                }

                await _repository.DeleteAsync<User>(user);
                NotifyOk(USER_DELETED);
            }
        }

        public async Task ChangePasswordAsync(UserPassword userPassword)
        {
            try
            {
                var valid = ValidateEntity<UserPassword>(_userPasswordValidator, userPassword);
                if (valid)
                {
                    if (CheckUpdatePasswordAuthorization(userPassword.Id))
                    {
                        var user = await _repository.Get<User>()
                                                    .FirstOrDefaultAsync(x => x.Id == userPassword.Id);

                        if (user.Password == Utils.Encript(userPassword.Password))
                        {
                            user.SetPassword(Utils.Encript(userPassword.NewPassword));
                            await _repository.EditAsync<User>(user);
                            NotifyOk(USER_UPDATED);
                        }
                        else
                        {
                            NotifyForbidden(INVALID_PASSWORD);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
            }
        }

        public async Task<bool> CheckUserNameExistAsync(string userName)
        {
            try
            {
                return await _repository.Get<User>()
                                        .AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
                return false;
            }
        }

        public async Task<long> TotalRegistersAsync()
        {
            try
            {
                var result = await _repository.Get<User>().CountAsync();
                return result;
            }
            catch (Exception exception)
            {
                NotifyInternalServerError(exception);
                return -1;
            }
        }
    }
}
