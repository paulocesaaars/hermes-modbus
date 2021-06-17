using Deviot.Hermes.ModbusTcp.Business.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Data
{
    public interface IRepository : IDisposable
    {
        DbContext DbContext { get; }

        IQueryable<TEntity> Get<TEntity>() where TEntity : EntityBase;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        Task EditAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
    }
}
