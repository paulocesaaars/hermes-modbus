using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> GetAsync(Guid id);

        Task<IEnumerable<UserInfo>> GetAllAsync();

        Task InsertAsync(User user);

        Task UpdateAsync(UserInfo userInfo);

        Task DeleteAsync(Guid id);

        Task ChangePasswordAsync(UserPassword userPassword);

        Task<bool> CheckUserNameExistAsync(string userName);

        Task<long> TotalRegistersAsync();
    }
}
