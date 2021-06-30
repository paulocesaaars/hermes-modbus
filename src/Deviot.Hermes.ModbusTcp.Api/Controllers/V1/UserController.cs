﻿using AutoMapper;
using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Bases;
using Deviot.Hermes.ModbusTcp.Api.ModelViews;
using Deviot.Hermes.ModbusTcp.Api.ModelViews.Bases;
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
        public async Task<ActionResult<UserInfoModelView>> GetAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                var userModelView = _mapper.Map<UserInfoModelView>(user);

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
        public async Task<ActionResult<UserInfoModelView>> GetAllAsync(int take = 1000, int skip = 0)
        {
            try
            {
                var users = await _userService.GetAllAsync(take, skip);
                var usersModelView = _mapper.Map<IEnumerable<UserInfoModelView>>(users);

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
        public async Task<ActionResult> PostAsync([FromBody] UserModelView userModelView)
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] UserInfoModelView userModelView)
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
        public async Task<ActionResult> ChangePasswordAsync([FromBody] UserPasswordModelView userPasswordModelView)
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
        public async Task<ActionResult<bool>> CheckUserNameExistAsync(string username)
        {
            try
            {
                var result = await _userService.CheckUserNameExistAsync(username);

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
        public async Task<ActionResult<long>> TotalRegistersAsync()
        {
            try
            {
                var result = await _userService.TotalRegistersAsync();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
