using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface IGenericRepository <T> where T : IEntity
    {
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>>? predicate = null);
        Task<T> GetEntityByID(Guid id);
        Boolean Any(Expression<Func<T, bool>> predicate);
        Task<T> GetEntityBy(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstEntityBy(Expression<Func<T, bool>> predicate);
        Task Insert(T entity);
        void Delete(T entity);
        Task Update(T entity);
        Task SaveChanges();
    }
}
