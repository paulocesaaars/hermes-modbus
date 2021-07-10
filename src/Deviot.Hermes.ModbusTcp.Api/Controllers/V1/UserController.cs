using AutoMapper;
using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Bases;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.Api.ViewModels.Bases;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;

        public UserController(INotifier notifier, 
                              IMapper mapper, 
                              ILogger<AuthController> logger,
                              IUserService userService
                             ) : base(notifier, mapper, logger)
        {
            _userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoViewModel>> GetAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetAsync(id);

                if (_notifier.HasNotifications)
                    return CustomResponse();

                var userModelView = _mapper.Map<UserInfoViewModel>(user);
                return CustomResponse(userModelView);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfoViewModel>>> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                if (_notifier.HasNotifications)
                    return CustomResponse();

                var usersModelView = _mapper.Map<IEnumerable<UserInfoViewModel>>(users);
                return CustomResponse(usersModelView);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserViewModel userModelView)
        {
            try
            {
                var user = _mapper.Map<User>(userModelView);
                await _userService.InsertAsync(user);

                return CustomResponse();
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UserInfoViewModel userModelView)
        {
            try
            {
                var user = _mapper.Map<UserInfo>(userModelView);
                await _userService.UpdateAsync(user);

                return CustomResponse();
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] EntityBaseModelView entityBaseModelView)
        {
            try
            {
                await _userService.DeleteAsync(entityBaseModelView.Id);

                return CustomResponse();
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] UserPasswordViewModel userPasswordModelView)
        {
            try
            {
                var userPassword = _mapper.Map<UserPassword>(userPasswordModelView);
                await _userService.ChangePasswordAsync(userPassword);

                return CustomResponse();
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("check-username/{username}")]
        public async Task<ActionResult<bool?>> CheckUserNameExistAsync(string username)
        {
            try
            {
                var result = await _userService.CheckUserNameExistAsync(username);

                if (_notifier.HasNotifications)
                    return CustomResponse();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("total-registers")]
        public async Task<ActionResult<long?>> TotalRegistersAsync()
        {
            try
            {
                var result = await _userService.TotalRegistersAsync();

                if (_notifier.HasNotifications)
                    return CustomResponse();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
