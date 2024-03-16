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
        IEnumerable<T> FindAll(Expression<Func<T, bool>>? predicate = null);
        //IEnumerable<T> GetAll();
        T GetEntityByID(Guid id);
        bool Any(Expression<Func<T, bool>> predicate);
        T GetEntityBy(Expression<Func<T, bool>> predicate);
        T GetFirstEntityBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void SaveChanges();
    }
}
