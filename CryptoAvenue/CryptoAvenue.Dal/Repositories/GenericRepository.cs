using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly CryptoAvenueDbContext context;
        private readonly DbSet<TEntity> dbSet;

        protected GenericRepository(CryptoAvenueDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if (predicate == null)
                return await dbSet.ToListAsync();

            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetEntityBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetEntityByID(Guid id)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> GetFirstEntityBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}
