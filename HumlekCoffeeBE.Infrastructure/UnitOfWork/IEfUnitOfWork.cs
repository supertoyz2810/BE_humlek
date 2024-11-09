using HumlekCoffeeBE.Base.Entities;
using HumlekCoffeeBE.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.UnitOfWork
{
    public interface IEfUnitOfWork : ITransactionAble
    {
        Task<int> SaveChangesAsync(bool useTenant = true);

        TRepository Repository<TRepository>();

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}
