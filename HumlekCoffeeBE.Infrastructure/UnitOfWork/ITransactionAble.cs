using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.UnitOfWork
{
    public interface ITransactionAble
    {
        /// <summary>
        /// Starts a new transaction.
        /// </summary>
        void BeginTransaction();

        Task BeginTransactionAsync();

        /// <summary>
        /// Applies the outstanding operations in the current transaction to the database.
        /// </summary>
        void CommitTransaction();

        Task CommitTransactionAsync();

        /// <summary>
        /// Discards the outstanding operations in the current transaction.
        /// </summary>
        void RollbackTransaction();

        Task RollbackTransactionAsync();

        /// <summary>
        /// Dispose Transaction
        /// </summary>
        void DisposeTransaction();

        Task DisposeTransactionAsync();
    }
}
