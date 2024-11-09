using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Interfaces;
using HumlekCoffeeBE.Base.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.UnitOfWork
{
    public class EfUnitOfWork : IDisposable, IEfUnitOfWork
    {
        private readonly DbContext _context;
        private bool disposed;
        private Dictionary<string, object> repositories;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EfUnitOfWork(
            DbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(bool useTenant = true)
        {
            //if (useTenant)
            //{
            //    var tenantId = _httpContextAccessor.HttpContext?.GetCurrentTenant();
            //    foreach (var entry in _context.ChangeTracker.Entries<IG1Tenant>().ToList())
            //    {
            //        switch (entry.State)
            //        {
            //            case EntityState.Added:
            //            case EntityState.Modified:
            //                entry.Entity.TenantId = Guid.Parse(tenantId);
            //                break;
            //        }
            //    }
            //}

            return await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            disposed = true;
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type.Name))
                repositories[type.Name] = new BaseRepository<TEntity>(_context, _httpContextAccessor);

            return (IBaseRepository<TEntity>)repositories[type.Name];
        }

        public TRepository Repository<TRepository>()
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();

            var typeOfTRepo = typeof(TRepository);
            string typeOfTRepoName = typeof(TRepository).Name;

            if (typeOfTRepo.GenericTypeArguments.Length > 0)
                typeOfTRepoName += $"|{typeOfTRepo.GenericTypeArguments[0].Name}";

            if (!repositories.ContainsKey(typeOfTRepoName))
            {
                var typesOfClassImplementForTRepo = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeOfTRepo.IsAssignableFrom(p) && p.IsClass)
                    .ToList();

                var repositoryInstance = Activator.CreateInstance(
                    typesOfClassImplementForTRepo.First(),
                    _context,
                    _httpContextAccessor);

                repositories.Add(typeOfTRepoName, repositoryInstance);
            }

            return (TRepository)repositories[typeOfTRepoName];
        }

        public void BeginTransaction() => _context.Database.BeginTransaction();

        public async Task BeginTransactionAsync() =>
            await _context.Database.BeginTransactionAsync();

        public void CommitTransaction() => _context.Database.CommitTransaction();

        public async Task CommitTransactionAsync() =>
            await _context.Database.CommitTransactionAsync();

        public void RollbackTransaction() => _context.Database.RollbackTransaction();

        public async Task RollbackTransactionAsync() =>
            await _context.Database.RollbackTransactionAsync();

        public void DisposeTransaction() => _context.Database.CurrentTransaction.Dispose();

        public async Task DisposeTransactionAsync() =>
            await _context.Database.CurrentTransaction.DisposeAsync();
    }
}
