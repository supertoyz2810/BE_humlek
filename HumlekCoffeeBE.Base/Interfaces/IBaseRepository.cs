using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> IQueryable(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           Expression<Func<TEntity, bool>> limitAccessedDataPredicate = null);

        void Insert(TEntity entity, Func<List<string>, bool> predicate = null);

        Task InsertAsync(TEntity entity, Func<List<string>, bool> predicate = null);

        void InsertRange(List<TEntity> entities, Func<List<string>, bool> predicate = null);

        Task InsertRangeAsync(List<TEntity> entities, Func<List<string>, bool> predicate = null);

        void Update(TEntity entity, Func<List<string>, bool> predicate = null);

        void UpdateRange(List<TEntity> entities, Func<List<string>, bool> predicate = null);

        void Delete(TEntity entity, Func<List<string>, bool> predicate = null);

        void DeleteRange(List<TEntity> entities, Func<List<string>, bool> predicate = null);

        void Remove(TEntity entity, Func<List<string>, bool> predicate = null);

        void RemoveRange(List<TEntity> entities, Func<List<string>, bool> predicate = null);

        DbSet<TEntity> Table { get; }

        TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        int Count(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        bool Any(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}
