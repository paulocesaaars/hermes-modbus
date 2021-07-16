using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Data
{
    public class Repository : IRepository
    {
        private const string GENERIC_ERROR = "Houve um erro na camada de infraestrutura";

        public DbContext DbContext { get; private set; }

        public Repository(ApplicationDbContext db)
        {
            DbContext = db;
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : EntityBase
        {
            try
            {
                return DbContext.Set<TEntity>();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            try
            {
                await DbContext.Set<TEntity>().AddAsync(entity);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task EditAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            try
            {
                DbContext.Entry(entity).State = EntityState.Deleted;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(GENERIC_ERROR, exception);
            }
        }

        public void Dispose()
        {
            if (DbContext != null)
                DbContext.Dispose();
        }
    }
}
