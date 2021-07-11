using Deviot.Hermes.ModbusTcp.Business.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Business.Interfaces
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> Get<TEntity>() where TEntity : EntityBase;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        Task EditAsync<TEntity>(TEntity entity) where TEntity : EntityBase;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
    }
}
