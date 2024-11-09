using HumlekCoffeeBE.Infrastructure.HumlekCoffeeBEDbContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IEfUnitOfWork
    {
    }

    public class UnitOfWork : EfUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(HCDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
        }
    }
}
