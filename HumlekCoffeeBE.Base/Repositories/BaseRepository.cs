using HumlekCoffeeBE.Base.Interfaces;
using HumlekCoffeeBE.Base.Query;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HumlekCoffeeBE.Base.Entities;
using System.Security.Claims;

namespace HumlekCoffeeBE.Base.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _table;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _table = _context.Set<TEntity>();
            _httpContextAccessor = httpContextAccessor;
        }

        protected DbContext DbContext
        {
            get
            {
                return _context;
            }
        }

        protected Guid GetCurrentUserIdFromHttpContext()
        {
            try
            {
                var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("Id").Value);

                return userId;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        protected string GetCurrentUserNameFromHttpContext()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value.ToString();

                return userId;
            }
            catch
            {
                return string.Empty;
            }
        }

        public IQueryable<TEntity> IQueryable(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
           Expression<Func<TEntity, bool>> limitAccessedDataPredicate = null)
        {
            IQueryable<TEntity> query = _table;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query.Where(r => !r.DeletedDate.HasValue);
        }

        public void Insert(TEntity entity, Func<List<string>, bool> predicate = null)
        {
            entity.SetCreator(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());
            _table.Add(entity);
        }

        public async Task InsertAsync(TEntity entity, Func<List<string>, bool> predicate = null)
        {
            entity.SetCreator(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());
            await _table.AddAsync(entity);
        }

        public void InsertRange(List<TEntity> entities, Func<List<string>, bool> predicate = null)
        {
            entities.ForEach(entity =>
            {
                entity.SetCreator(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());
            });
            _table.AddRange(entities);
        }

        public async Task InsertRangeAsync(List<TEntity> entities, Func<List<string>, bool> predicate = null)
        {
            entities.ForEach(entity =>
            {
                entity.SetCreator(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());
            });
            await _table.AddRangeAsync(entities);
        }

        public void Update(TEntity entity, Func<List<string>, bool> predicate = null)
        {
            entity.SetModifier(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());

            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(List<TEntity> entities, Func<List<string>, bool> predicate = null)
        {
            entities.ForEach(entity =>
            {
                entity.SetModifier(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());
            });
            _table.UpdateRange(entities);
        }

        public void Delete(TEntity entity, Func<List<string>, bool> predicate = null)
        {
            entity.SetRemover(GetCurrentUserIdFromHttpContext(), GetCurrentUserNameFromHttpContext());

            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteRange(List<TEntity> entities, Func<List<string>, bool> predicate = null)
        {
            entities.ForEach(entity =>
            {
                Delete(entity, predicate);
            });
        }

        public void Remove(TEntity entity, Func<List<string>, bool> predicate = null)
        {
            _table.Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities, Func<List<string>, bool> predicate = null)
        {
            _table.RemoveRange(entities);
        }

        public DbSet<TEntity> Table
        {
            get
            {
                return _table;
            }
        }

        public TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            => IQueryable(include: include).FirstOrDefault(predicate);

        public async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            => await IQueryable(include: include).FirstOrDefaultAsync(predicate);

        public TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            => IQueryable(include: include).SingleOrDefault(predicate);

        public async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) =>
            await IQueryable(include: include).SingleOrDefaultAsync(predicate);

        public int Count(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null)
                return IQueryable(include: include).Count();
            else
                return IQueryable(include: include).Count(predicate);
        }

        public async Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null)
                return await IQueryable(include: include).CountAsync();
            else
                return await IQueryable(include: include).CountAsync(predicate);
        }

        public bool Any(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null)
                return IQueryable(include: include).Any();
            else
                return IQueryable().Any(predicate);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (predicate == null)
                return await IQueryable(include: include).AnyAsync();
            else
                return await IQueryable(include: include).AnyAsync(predicate);
        }
    }
}
