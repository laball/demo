using System;
using NHibernate;

namespace Core.Dao
{
    /// <summary>
    /// NHibernate Unit Of Work 实现
    /// </summary>
    public class NHUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="NHUnitOfWork"/> class.
        /// </summary>
        /// <param name="session">NHibernate Session</param>
        public NHUnitOfWork(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("UnitOfWork have already been saved.");
            }

            _transaction.Commit();
            _transaction = null;
        }
    }
}
